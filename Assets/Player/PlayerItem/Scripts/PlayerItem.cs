using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerItem : MonoBehaviour
{
    public string itemName;

    public string describe;

    public int count;

    public string itemType;

    public Sprite itemSprite;

    public GameObject ItemInfoForm;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //检测是否第一次生成
        if(ItemManager.Instance.itemDic.ContainsKey(gameObject.name))
        {
            count=ItemManager.Instance.itemDic[gameObject.name];
            // Debug.Log(count);
        }
        else
        {
            ItemManager.Instance.itemDic.Add(gameObject.name,1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //获取物品的数据
    public virtual GameObject ReportItemInfo()
    {
        
        return null;
    } 
}
