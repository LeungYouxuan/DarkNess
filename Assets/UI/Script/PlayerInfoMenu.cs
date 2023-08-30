using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInfoMenu : UIPanel
{    
    public Text playerName;
    public Text playerDeposit;
    public Text playerReputation;
    public List<ItemBox>itemBoxList;
    public KeyCode  responseKey=KeyCode.B;
    protected override void Start()
    {
        canCover=true;
        level=0;
        GetComponent<RectTransform>().localPosition=new Vector3(0,0,0);
        GetComponent<RectTransform>().sizeDelta=new Vector2(296,161);   
        if(PlayerControl.Instance!=null)
        {
            playerName.text="姓名:"+PlayerControl.Instance.playerName;
            playerDeposit.text="存款:"+PlayerControl.Instance.money;
            playerReputation.text="声望"+PlayerControl.Instance.playerReputation;
        }
        //给背包格子进行编号
        for(int i=0;i<itemBoxList.Count;i++)
        {
            itemBoxList[i].id=i;
        }
        //循环地给背包格子填充物品
        for(int i=0;i<ItemManager.Instance.realItemList.Count;i++)
        {
            itemBoxList[i].count.gameObject.SetActive(true);
            itemBoxList[i].count.text=ItemManager.Instance.realItemList[i].GetComponent<PlayerItem>().count.ToString();
            itemBoxList[i].itemSprite.sprite=ItemManager.Instance.realItemList[i].GetComponent<PlayerItem>().itemSprite;
            itemBoxList[i].itemSprite.color=new Color32(255,255,255,255);                
        }                
    }
}
