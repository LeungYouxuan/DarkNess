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
        EventManager.Instance.AddEventListener("ReturnToGame",WhenGameReStart);
        EventManager.Instance.AddEventListener("PauseGame",WhenGamePause);
    }
    // Update is called once per frame
    void Update()
    {

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
    IEnumerator LoadScene(string target)
    {
        yield return SceneManager.LoadSceneAsync(target);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(target));
        LoaderManager.Instance.Save();
    }
    public void WhenGamePause()
    {
        //这里写上游戏暂停时的操作
        Time.timeScale=0;
    }
    public void WhenGameReStart()
    {
        //这里写上游戏恢复时的操作
        Time.timeScale=1;
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

}
