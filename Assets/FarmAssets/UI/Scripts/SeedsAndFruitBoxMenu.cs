using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Http.Headers;
public class SeedsAndFruitBoxMenu : UIPanel
{
    public Text currentSeedName;
    public Text nextSeedName;
    public Text totalSeedsCount;
    public Text gameTotalTimeText;
    public GameObject lastTimeSilder;
    private float gameTotalTime;
    public Text score;
    private void Awake() {
        
    }
    protected override void Start()
    {
        base.Start();
        level=1;
        canCover=true;
        UpadateInfomation();
        GetComponent<RectTransform>().localPosition=new Vector3(0,0,0);
        GetComponent<RectTransform>().sizeDelta=new Vector2(0,1080);
        
    }   

    // Update is called once per frame
    void Update()
    {
        if(GridManager.Instance!=null)
        {
            lastTimeSilder.GetComponent<Image>().fillAmount=GridManager.Instance.gameTotalTime/gameTotalTime;
            gameTotalTimeText.text="剩余时间"+'\n'+GridManager.Instance.gameTotalTime.ToString();            
        }

    }
    public void UpadateInfomation()
    {
        score.text="得分:"+'\n'+GridManager.Instance.score.ToString();
        gameTotalTime=GridManager.Instance.gameTotalTime;
        gameTotalTimeText.text="剩余时间"+'\n'+GridManager.Instance.gameTotalTime.ToString();
        if(GridManager.Instance.currentSeed!=null)
            currentSeedName.text="当前种子："+'\n'+GridManager.Instance.currentSeed.GetComponent<Seeds>().name;
        else
            currentSeedName.text="当前种子："+'\n'+"NULL";
        if(GridManager.Instance.seedsQueue.Count!=0)
            nextSeedName.text="下一种子："+'\n'+GridManager.Instance.seedsQueue.Peek().GetComponent<Seeds>().name;
        else
            nextSeedName.text="下一种子："+'\n'+"NULL";
        totalSeedsCount.text="当前种子总数量"+'\n'+(GridManager.Instance.totalSeedsCount).ToString();
    }
}
