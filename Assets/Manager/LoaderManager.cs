using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using System.IO;
public class LoaderManager : Singleton<LoaderManager>
{
    public string jsonFloder;

    private List<ISaveable>saveableList=new List<ISaveable>();
    
    private Dictionary<string,GameSaveData>saveDataDic=new Dictionary<string, GameSaveData>();

    
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        jsonFloder=Application.persistentDataPath +"/SAVE/";
    }

    void Start()
    {
        //加载数据的操作
        EventManager.Instance.AddEventListener("NewGameStart",OnNewGameStart);
        //
    }

    // Update is called once per frame
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
        if(File.Exists(resultPath))
        {
            File.Delete(resultPath);
            Debug.Log("删除原存档");
        }
        //卸载完数据后加载第一个主场景
        GameManager.Instance.currentScene="MainScene";
        GameManager.Instance.SceneJump("StartScene","MainScene");
    }
    public void Save()
    {
        //saveDataDic.Clear();
        //列表里的GameSaveData只要不被清空，那么就不会出现Save新场景会删除旧场景的内容
        foreach(var saveable in saveableList)
        {
            
            if(saveDataDic.ContainsKey(saveable.GetType().Name))
            {
                saveDataDic[saveable.GetType().Name]=saveable.GenerateSaveData();
            }
            else
            {
                saveDataDic.Add(saveable.GetType().Name,saveable.GenerateSaveData());
            }
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
