using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIPanel : UIPanel
{
    public Text moneyText;

    public GameObject missonMenu;
    public GameObject openMission;
    protected override void Start() 
    {
        base.Start();
        level=4;
        canCover=true;
        GetComponent<RectTransform>().offsetMax=new Vector2(GetComponent<RectTransform>().offsetMax.x,0);
        moneyText.text="金钱:"+PlayerControl.Instance.money.ToString();
        openMission.GetComponent<Button>().onClick.AddListener(OpenMissionPanel);
        openMission.GetComponent<RectTransform>().anchoredPosition=new Vector2(46,-280);
        Debug.Log("生成UI");
    }
    public void OpenMissionPanel()
    {
        UIManager.Instance.AddUiPanel("MissionMenu");
    }
}
