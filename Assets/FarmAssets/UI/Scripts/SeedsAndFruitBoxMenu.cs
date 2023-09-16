using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedsAndFruitBoxMenu : UIPanel
{
    public Text currentSeedName;
    public Text nextSeedName;
    public Text totalSeedsCount;
    protected override void Start()
    {
        base.Start();
        level=1;
        canCover=true;
        //UpadateInfomation();
        EventManager.Instance.AddEventListener("UpdateGridGameUI",UpadateInfomation);
        EventManager.Instance.TriggerEventListener("UpdateGridGameUI");
    }   

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpadateInfomation()
    {
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
