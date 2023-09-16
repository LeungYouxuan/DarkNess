using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : ItemOnWorld
{
    public int id;
    public GameObject thisGridSeed;
    void Start()
    {
        if(GridManager.Instance!=null)
        {
            //GridManager.Instance.gridList.Add(this.gameObject);
            GridManager.Instance.gridPositonDic.Add(this.gameObject.transform.localPosition, this.gameObject);
            canInteract=true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //如果耕地被种下了种子，就马上开始种子生长计时
        if(canInteract==false)
        {
            if(thisGridSeed.GetComponent<Seeds>().thisSeedsData.isRipe==false)
                StartCoroutine(thisGridSeed.GetComponent<Seeds>().StartGrow());
        }
    }
    public override void Interaction()
    {
        if(canInteract&&GridManager.Instance.currentSeed!=null)
        {
            Debug.Log("种菜!");
            //在种子箱的队头取出种子,挂载在耕地格子下
            thisGridSeed=Instantiate(GridManager.Instance.currentSeed);
            thisGridSeed.transform.parent=transform;
            thisGridSeed.transform.position=transform.position;
            canInteract=false;
            CursorManager.Instance.canClick=true;
            GridManager.Instance.AfterPlant();
            // var clearList=GridManager.Instance.MatchGrids(this.gameObject);
            // if(clearList.Count!=0)
            // {
            //     //这里执行清除操作,清除的是耕地上种植的作物
            //     //播放消除动画
            //     foreach(var item in clearList)
            //     {
            //         //Debug.Log(item.transform.GetChild(0).name);
            //         Destroy(item.transform.GetChild(0).gameObject);
            //         item.GetComponent<Grid>().canInteract=true;
            //     }
            // }
        }
        else
        {
            CursorManager.Instance.canClick=true;
        }
    }
}
