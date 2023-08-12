using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using System.IO;
public class LoaderManager : Singleton<LoaderManager>
{
    public string jsonFloder;

    public List<ISaveable>saveableList;
    
    public Dictionary<string,GameSaveData>saveDataDic;

    
    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(gameObject);
        jsonFloder=Application.persistentDataPath+"/SAVE/";
        saveableList=new List<ISaveable>();
        saveDataDic=new Dictionary<string, GameSaveData>();
    }

    void Start()
    {
        EventManager.Instance.AddEventListener("NewGameStart",OnNewGameStart);
    }

    void Update()
    {
        
    }
    public void Register(ISaveable saveable)
    {
        int isContain=0;
        for(int i=0;i<saveableList.Count;i++)
        {
            if(saveable.GetType()==saveableList[i].GetType())
            {
                saveableList.RemoveAt(i);
                saveableList.Add(saveable);
                isContain=1;
                break;
            }
        }
        if(isContain==0)
        {
            saveableList.Add(saveable);
        }
    }
    public void OnNewGameStart()
    {
        var resultPath=jsonFloder+"data.sav";
        File.Delete(resultPath);
        Debug.Log("删除原存档");
        Debug.Log("开始新存档");      
    }
    public void Save()
    {
        saveDataDic.Clear();
        foreach(var saveable in saveableList)
        {
            saveDataDic.Add(saveable.GetType().Name,saveable.GenerateSaveData());
        }
        var resultPath=jsonFloder+"data.sav";

        var jsonData=JsonConvert.SerializeObject(saveDataDic,Formatting.Indented);

        if(!File.Exists(resultPath))
        {
            Directory.CreateDirectory(jsonFloder);
        }
        File.WriteAllText(resultPath,jsonData);
        Debug.Log("保存成功");
    }
    public void Load()
    {
        var resultPath=jsonFloder+"data.sav";
        if(!File.Exists(resultPath))
        {
            return;
        }
        else
        {
            var stringData=File.ReadAllText(resultPath);

            var jsonData=JsonConvert.DeserializeObject<Dictionary<string,GameSaveData>>(stringData);

            foreach(var saveable in saveableList)
            {
                saveable.RestoreGameSaveData(jsonData[saveable.GetType().Name]);
            }
            Debug.Log("加载成功");
        }
    }
}
