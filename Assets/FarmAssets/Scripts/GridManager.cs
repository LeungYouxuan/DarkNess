using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : Singleton<GridManager>
{
    public int totalSeedsCount;
    public float gameTotalTime=25;
    public int score=0;
    private bool isGameEnd;
    [Header("地图列表，拖入地图预制体即可，在GroundPrefabs当中")]
    public List<GameObject>groundList=new List<GameObject>();
    [Header("开局的种子列表，拖入种子预制体即可，在Seed/SeedsPrefabs当中")]
    public List<GameObject>seedsList=new List<GameObject>();//种子列表，方便填入队列
    public Queue<GameObject>seedsQueue=new Queue<GameObject>();//选种子队列
    //public Dictionary<string,Fruit_SO>fruitDic=new Dictionary<string, Fruit_SO>();//收成的果实仓库字典
    //public Dictionary<string,SeedsBox_SO>seedsDic=new Dictionary<string, SeedsBox_SO>();//种子仓库字典
    public GameObject currentSeed;
    [Header("果实仓库配置表模板")]
    public Fruit_SO templateFruitBox;
    [Header("种子仓库配置表模板")]
    public SeedsBox_SO templateSeedsBox;
    public Dictionary<Vector2,GameObject>gridPositonDic=new Dictionary<Vector2, GameObject>();
    [Header("选择第几个地图模板,index从0开始")]
    public int index;
    protected override void Awake()
    {
        base.Awake();
        //index=Random.Range(0,3);
        foreach(var seeds in seedsList)
        {
            seedsQueue.Enqueue(seeds);
        }
        //初始化队头,初始化当前种子
        currentSeed=seedsQueue.Dequeue();
        totalSeedsCount=seedsQueue.Count+1;

    }
    void Start()
    {
        Instantiate(groundList[index],new Vector3(0,0,0),Quaternion.identity);
        UIManager.Instance.AddUiPanel("SeedsAndFruitBoxMenu");
        Debug.Log(gridPositonDic.Count);
        Debug.Log("GridManager:"+"加载UI");
        //Destroy(ground);
    }
    
    void Update() 
    {
        if(gameTotalTime>0)
            StartCoroutine(StartTimer());
        //如果种子种完了，并且剩余时间大于10s，那就把剩余时间直接缩小到10s
        if(totalSeedsCount<=0&&gameTotalTime>10)
        {
            gameTotalTime=10f;
        }
        if(isGameEnd)
        {
            //刷新游戏
            if(Input.GetKeyDown(KeyCode.Home))
            {
                UIManager.Instance.PopUIPanel();
                Destroy(GameObject.FindGameObjectWithTag("Ground"));
                TransitionManager.Instance.SceneJump("Farm","Farm");
            }

        }
    }
    IEnumerator StartTimer()
    {
        gameTotalTime-=Time.deltaTime;
        yield return new WaitForEndOfFrame();
        if(gameTotalTime<=0)
        {
            AfterFarmGameEnd();
            
        }
    }
    public void AfterFarmGameEnd()
    {
        //统计场上剩余的种子或者果实并清空
        //若种子箱内还有种子，也要进行清空
        GameObject[]lastCrop=GameObject.FindGameObjectsWithTag("Crop");
        foreach(var go in lastCrop)
        {
            Debug.Log(go.name+go.transform.localPosition);
            Destroy(go.gameObject);
        }
        currentSeed=null;
        if(seedsQueue.Count!=0)
        {
            seedsQueue.Clear();
        }
        isGameEnd=true;
    }
    public void AfterPlant()
    {
        if(totalSeedsCount>2)
        {
            currentSeed=seedsQueue.Dequeue();
            totalSeedsCount=seedsQueue.Count+1;
            GameObject.FindGameObjectWithTag("FarmUI").GetComponent<SeedsAndFruitBoxMenu>().UpadateInfomation();
        }
        else if (totalSeedsCount==2)
        {
            currentSeed=seedsQueue.Dequeue();
            totalSeedsCount=seedsQueue.Count+1;
            Debug.Log("种子箱已经没有种子了,无法再为您提供种子");
            GameObject.FindGameObjectWithTag("FarmUI").GetComponent<SeedsAndFruitBoxMenu>().UpadateInfomation();
        }
        else if(totalSeedsCount==1)
        {
            totalSeedsCount=0;
            currentSeed=null;
            GameObject.FindGameObjectWithTag("FarmUI").GetComponent<SeedsAndFruitBoxMenu>().UpadateInfomation();
            Debug.Log("种子箱彻底被榨干了");
        }
    }
    //收获函数
    public void HarvestCrop(GameObject checkGamObject)
    {
        int value=checkGamObject.GetComponent<Grid>().thisGridSeed.GetComponent<Seeds>().thisSeedsData.seedValue;
        var clearList=MatchGrids(checkGamObject);
        if(clearList.Count>0)
        {
            foreach(var grid in clearList)
            {
                //解锁耕地
                grid.GetComponent<Grid>().canInteract=true;
                //触发收成事件，把作物果实和种子分别送入果实仓库和种子仓库
                //SendFruit(checkGamObject);
                //SendSeeds(checkGamObject);
                //销毁作物
                Destroy(grid.transform.GetChild(0).gameObject);
                
            }
            //统计得分
            EventManager.Instance.TriggerEventListener("UpdateGridGameUI");
            score+=value*clearList.Count;
        }
    }
    //把收获的果实送入果实仓库
    // private void SendFruit(GameObject go)
    // {
    //     //如果果实仓库还未创建:
    //     if(!fruitDic.ContainsKey(go.transform.GetChild(0).gameObject.
    //     GetComponent<Seeds>().thisSeedsData.fruitName))
    //     {
    //         //把果实送入新创建的仓库,背后逻辑是对应果实仓库下的count字段++；
    //         var box=Instantiate(templateFruitBox);
    //         fruitDic.Add(go.transform.GetChild(0).gameObject.GetComponent<Seeds>().thisSeedsData.fruitName,box);
    //         fruitDic[go.transform.GetChild(0).gameObject.GetComponent<Seeds>().thisSeedsData.fruitName].fruitCount++;
    //     }
    //     else
    //     {
    //         fruitDic[go.transform.GetChild(0).gameObject.GetComponent<Seeds>().thisSeedsData.fruitName].fruitCount++;
    //     }
    //     Debug.Log(go.transform.GetChild(0).gameObject.GetComponent<Seeds>().thisSeedsData.fruitName+"仓库中含有:"
    //     +fruitDic[go.transform.GetChild(0).gameObject.GetComponent<Seeds>().thisSeedsData.fruitName].fruitCount);
    // }
    //把收获的种子送入种子仓库
    // private void SendSeeds(GameObject go)
    // {
    //     if(!seedsDic.ContainsKey(go.transform.GetChild(0).gameObject.GetComponent<Seeds>().thisSeedsData.type))
    //     {
    //         //把收获的种子送入新创建的种子仓库
    //         var box=Instantiate(templateSeedsBox);
    //         seedsDic.Add(go.transform.GetChild(0).gameObject.GetComponent<Seeds>().thisSeedsData.type,templateSeedsBox);
    //         seedsDic[go.transform.GetChild(0).gameObject.GetComponent<Seeds>().thisSeedsData.type].seedsCount++;
    //         seedsDic[go.transform.GetChild(0).gameObject.GetComponent<Seeds>().thisSeedsData.type].type=go.transform.GetChild(0).gameObject.GetComponent<Seeds>().thisSeedsData.type;
    //     }
    //     else
    //     {
    //         seedsDic[go.transform.GetChild(0).gameObject.GetComponent<Seeds>().thisSeedsData.type].seedsCount++;
    //     }
    //     Debug.Log(go.transform.GetChild(0).gameObject.GetComponent<Seeds>().thisSeedsData.type+"仓库中含有:"+
    //     seedsDic[go.transform.GetChild(0).gameObject.GetComponent<Seeds>().thisSeedsData.type].seedsCount);
    // }
    #region 收成检测
    public List<GameObject>RowMatch(GameObject checkedGameObject)
    {
        List<GameObject>matchRowGrids=new List<GameObject>();
        matchRowGrids.Add(checkedGameObject);
        for(int i=0;i<=1;i++)
        {
            for(int xDistance=1;xDistance<3.5;xDistance++)
            {
                float x;
                //向左遍历
                if(i==0)
                {
                    x=checkedGameObject.transform.localPosition.x-xDistance;
                }
                //向右遍历
                else
                {
                    x=checkedGameObject.transform.localPosition.x+xDistance;
                }
                //边界判断
                if(x<-3.5||x>3.5)
                {
                    break;
                }
                if(gridPositonDic.ContainsKey(new Vector2(x,checkedGameObject.transform.localPosition.y)))
                {
                    //如果没有种植作物
                    if(gridPositonDic[new Vector2(x,checkedGameObject.transform.localPosition.y)].GetComponent<Grid>().thisGridSeed==null)
                    {
                        break;
                    }
                    //如果邻接的作物匹配,并且都成熟
                    else if(gridPositonDic[new Vector2(x,checkedGameObject.transform.localPosition.y)].GetComponent<Grid>().thisGridSeed.GetComponent<Seeds>().thisSeedsData.type
                    ==checkedGameObject.GetComponent<Grid>().thisGridSeed.GetComponent<Seeds>().thisSeedsData.type)
                    { 
                        Debug.Log("为同类型作物！并且均成熟");
                        matchRowGrids.Add(gridPositonDic[new Vector2(x,checkedGameObject.transform.localPosition.y)]);
                    }
                    //不是耕地
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
        return matchRowGrids;
    }
    public List<GameObject>ColMatch(GameObject checkedGameObject)
    {
        
        List<GameObject> matchColGrids = new List<GameObject>();
        matchColGrids.Add(checkedGameObject);
        for(int i=0;i<=1;i++)
        {
            for(int yDistance=1;yDistance<3.5;yDistance++)
            {
                float y;
                if(i==0)
                {
                    y=checkedGameObject.transform.localPosition.y-yDistance;
                }
                else
                {
                    y=checkedGameObject.transform.localPosition.y+yDistance;
                }
                //边界判断
                if(y<-3.5||y>3.5)
                {
                    break;
                }
                if(gridPositonDic.ContainsKey(new Vector2(checkedGameObject.transform.localPosition.x,y)))
                {
                    //如果没有种植作物
                    if(gridPositonDic[new Vector2(checkedGameObject.transform.localPosition.x,y)].GetComponent<Grid>().thisGridSeed==null)
                    {
                        break;
                    }
                    //如果邻接的作物匹配
                    else if(gridPositonDic[new Vector2(checkedGameObject.transform.localPosition.x,y)].GetComponent<Grid>().thisGridSeed.GetComponent<Seeds>().thisSeedsData.type
                    ==checkedGameObject.GetComponent<Grid>().thisGridSeed.GetComponent<Seeds>().thisSeedsData.type)
                    {
                        Debug.Log("为同类型植物！");
                        matchColGrids.Add(gridPositonDic[new Vector2(checkedGameObject.transform.localPosition.x,y)]);
                    }
                    //不是耕地
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        } 
        return matchColGrids;       
    }
    public List<GameObject> MatchGrids(GameObject checkedGameObject)
    {
        List<GameObject>matchRowGrids=new List<GameObject>();//横向匹配的元素
        List<GameObject>matchColGrids=new List<GameObject>();//纵向匹配的元素
        List<GameObject>clearGrids=new List<GameObject>();//最终消除的元素
        matchRowGrids=RowMatch(checkedGameObject);
        if(matchRowGrids.Count>=checkedGameObject.GetComponent<Grid>().thisGridSeed.GetComponent<Seeds>().thisSeedsData.clearCount)
        {
            foreach(var go in matchRowGrids)
            {
                clearGrids.Add(go);
            }
            matchColGrids=ColMatch(checkedGameObject);
            if(matchColGrids.Count >=checkedGameObject.GetComponent<Grid>().thisGridSeed.GetComponent<Seeds>().thisSeedsData.clearCount)
            {
                foreach(var go in matchColGrids)
                {
                    clearGrids.Add(go);
                }
            }
        }
        else
        {
            matchColGrids=ColMatch(checkedGameObject);
            Debug.Log(matchColGrids.Count);
            if(matchColGrids.Count >=checkedGameObject.GetComponent<Grid>().thisGridSeed.GetComponent<Seeds>().thisSeedsData.clearCount)
            {
                foreach(var go in matchColGrids)
                {
                    clearGrids.Add(go);
                }
            }
        }
        return clearGrids;
    }
    #endregion
}
