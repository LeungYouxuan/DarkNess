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
        LoaderManager.Instance.Load();
    }
    // Update is called once per frame
    void Update()
    {

    }
    //场景的管理
    public void SceneJump(string from,string target)
    {
        StartCoroutine(Transition(from,target));
        
    }
    private IEnumerator UnLoadScene(string from,string target)
    {     
        AsyncOperation operation=SceneManager.UnloadSceneAsync(from);
        if(!operation.isDone)
        {
            Debug.Log("正在卸载场景");
            yield return null;
        }
        Debug.Log("已经卸载了场景");
                  
    }
    IEnumerator LoadScene(string target)
    {
        yield return SceneManager.LoadSceneAsync(target);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(target));
        LoaderManager.Instance.Load();
    }
    IEnumerator Transition(string from,string target)
    {
        yield return SceneManager.UnloadSceneAsync(from);
        yield return SceneManager.LoadSceneAsync(target,LoadSceneMode.Additive);
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount-2);
        SceneManager.SetActiveScene(newScene);
        LoaderManager.Instance.Load();
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
