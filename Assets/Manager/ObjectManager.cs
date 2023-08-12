using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>,ISaveable
{
    //存储世界上可互动物品的状态的字典，string对应物品的名字，bool对应的是状态canOperate,这样才能被序列化存储
    public Dictionary<string,bool>itemOnWorldStituationDic=new Dictionary<string, bool>();
    //存储世界上可互动物品的实体的列表
    public List<ItemOnWorld>itemOnWorldList;
    protected override void Awake()
    {   
        base.Awake();
    }
    void Start()
    {
        ISaveable saveable=this;
        saveable.SaveableRegister();
        //激活后第一件事就是要添加物品更新时执行的委托
        //EventManager.Instance.AddEventListener(UpadeSituation);
    }  
    void Update()
    {

    }
    public GameSaveData GenerateSaveData()
    {
        GameSaveData data =new GameSaveData();
        data.itemOnWorldStituationDic=itemOnWorldStituationDic;
        return data;
    }

    public void RestoreGameSaveData(GameSaveData data)
    {
        itemOnWorldStituationDic=data.itemOnWorldStituationDic;
    }  
    //真正的把世界上各可互动物品的状态写入字典便于存储,这里是全局更新，当前世界上所有物品的状态会被存储
    public void SaveAllteItemSituation()
    {
        foreach(var item in itemOnWorldList)
        {
            //注意这里要判重，因为有些物品会被多次更新状态,用最新的一次存储覆盖
            if(itemOnWorldStituationDic.ContainsKey(item.name))
            {
                itemOnWorldStituationDic[item.name]=item.canInteract;
                Debug.Log("物品:"+item.name+"发生重复状态更新存储");
            }
            else
            {
                itemOnWorldStituationDic.Add(item.name,item.canInteract);
                Debug.Log("物品:"+item.name+"发生第一次状态更新存储");
            }
        }
    }  
    //单次存储某物品的状态，需要保存物品作为形参,保存的逻辑是一样的
    public void SaveItemSituation(ItemOnWorld item)
    {
        if(itemOnWorldStituationDic.ContainsKey(item.name))
        {
            itemOnWorldStituationDic[item.name]=item.canInteract;
            Debug.Log("物品:"+item.name+"发生重复状态更新存储，当前状态为:"+item.canInteract);
        }
        else
        {
            itemOnWorldStituationDic.Add(item.name,item.canInteract);
            Debug.Log("物品:"+item.name+"发生第一次状态更新存储当前状态为:"+item.canInteract);
        }        
    }
    //更新物品的状态到物品的实体
    public void UpdateItemSituation()
    {
        //需要判断物体是否存在于当前场景,因此先获得当前场景所有的物体,这里返回的是一个指定类型数组（ItemOnWorld）
        var itemArray=GameObject.FindObjectsOfType<ItemOnWorld>();
        Debug.Log(itemArray.Length);
        foreach(var item in itemOnWorldList)
        {
            int j=0;
            for(int i=0;i<itemArray.Length-j;i++)
            {
                //每次判断是否存在，伴随着的是一次状态更新，此时要判断该物体是否已经存在于字典，若没有，则需要写入字典,若有，则把字典里的状态更新到物体实体
                if(itemArray[i].name==item.name&&itemOnWorldStituationDic.ContainsKey(item.name))
                {
                    itemArray[i].canInteract=itemOnWorldStituationDic[item.name];
                    Debug.Log("物体："+item.name+"当前状态应为："+itemOnWorldStituationDic[item.name]);
                }
                else if(itemArray[i].name==item.name&&itemOnWorldStituationDic.ContainsKey(item.name)==false)
                {
                    itemOnWorldStituationDic.Add(item.name,item.canInteract);
                    Debug.Log("物体："+item.name+"当前状态应为："+itemOnWorldStituationDic[item.name]);
                }
            }
        }
    }
}
