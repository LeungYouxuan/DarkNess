using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>,ISaveable
{
    public List<GameObject>itemList;
    
    public Dictionary<string,int>itemDic=new Dictionary<string, int>();

    public List<GameObject>realItemList;
    protected override void Awake() 
    {
        base.Awake();
        realItemList=new List<GameObject>();
        //这里可以做背包默认物品加载操作
        itemDic.Add("Rifle",3);
    }
    private void Start() 
    {
        //生成物品实体挂载在ItemManager下
        realItemList=FindItemsInItemList();
        for(int i=0;i<realItemList.Count;i++)
        {
            Debug.Log("当前存储字典中有:"+realItemList.Count+"个玩家物品对象");
            var go=ObjectPoolManager.Instance.GetInstance(realItemList[i].name,realItemList[i]);
            //通过预制体生成的物体名字带有Clone字符串，因此最好做一下处理.
            go.gameObject.name=realItemList[i].name;
            go.gameObject.transform.SetParent(gameObject.transform);
            //这里记得！生成的物品要覆盖realItemList，因为此前realItemList是拷贝itemList的，仅仅只是初始化的预制体，并没有读取数据
            realItemList[i]=go;
        }
    }
    public GameSaveData GenerateSaveData()
    {
        GameSaveData data=new GameSaveData();
        data.playerItemDic=itemDic;
        return data;
    }
    public void RestoreGameSaveData(GameSaveData data)
    {
        itemDic=data.playerItemDic;
    }
    public List<GameObject> FindItemsInItemList()
    {
        List<GameObject>gameObjectsList =new List<GameObject>();
        foreach(GameObject item in itemList)
        {
            if(itemDic.ContainsKey(item.name))
            {
                gameObjectsList.Add(item);
            }
        }
        return gameObjectsList;
    }
}

