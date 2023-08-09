using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorldManager : Singleton<ItemOnWorldManager>,ISaveable
{
    public Dictionary<string,bool>itemStatusDic;
    protected override void Awake() {
        ISaveable saveable;
        saveable=this;
        saveable.SaveableRegister();
    }
    private void Start() {
        
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData gameSaveData=new GameSaveData();
        gameSaveData.itemStatusDic=itemStatusDic;
        return gameSaveData;
    }

    public void RestoreGameSaveData(GameSaveData data)
    {
        data.itemStatusDic=itemStatusDic;
    }
}
