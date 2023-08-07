using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>,ISaveable
{
   public string currentScene;
   protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        ISaveable saveable =this;
        saveable.SaveableRegister();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Instance.SceneJump("MainScene","AnotherScene"); 
            
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Instance.SceneJump("AnotherScene","MainScene");
            
        }
    }
    //场景的管理
    public void SceneJump(string from,string target)
    {
        StartCoroutine(UnLoadScene(from,target));//加载和卸载场景
        StartCoroutine(LoadScene(target));
    }
    private IEnumerator UnLoadScene(string from,string target)
    {
        //卸载场景之前先保存数据
        currentScene=target;
        LoaderManager.Instance.Save();        
        AsyncOperation operation=SceneManager.UnloadSceneAsync(from);
        if(!operation.isDone)
        {
            Debug.Log("正在卸载场景");
            yield return null;
        }
        Debug.Log("已经卸载了场景");
        //加载完场景后先保存新场景的数据.          
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData newGameSaveData = new GameSaveData();
        newGameSaveData.currentScene=this.currentScene;
        return newGameSaveData;
    }

    public void RestoreGameSaveData(GameSaveData data)
    {
        currentScene=data.currentScene;
    }
    IEnumerator LoadScene(string target)
    {
        yield return SceneManager.LoadSceneAsync(target);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(target));
        LoaderManager.Instance.Save();
    }
}
