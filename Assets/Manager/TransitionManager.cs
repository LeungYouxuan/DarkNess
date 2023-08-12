using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionManager : Singleton<TransitionManager>,ISaveable
{
    public string currentScene;
    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        currentScene="MainScene";
    }

    void Update() 
    {
        
    }

    //异步卸载加载场景
    public void SceneJump(string from,string target)
    {
        StartCoroutine(Transition(from,target));
    }
    IEnumerator Transition(string from,string target)
    {
        if(from!="")
        {
            //这里写卸载场景之前执行的事情
            LoaderManager.Instance.Save();
            //卸载场景
            yield return SceneManager.UnloadSceneAsync(from);
            //加载场景
            yield return SceneManager.LoadSceneAsync(target,LoadSceneMode.Additive);
            //这里写加载场景后执行的事情
            SceneManager.sceneLoaded+=(Scene loadScene,LoadSceneMode loadSceneMode)=>
            {
                LoaderManager.Instance.Load();
            };
        }
    }
    public GameSaveData GenerateSaveData()
    {
        GameSaveData data=new GameSaveData();
        data.currentScene=currentScene;
        return data;
    }

    public void RestoreGameSaveData(GameSaveData data)
    {
        currentScene=data.currentScene;
    }
}
