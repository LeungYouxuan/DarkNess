using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>,ISaveable
{
   public string currentSceneName;
   protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        if(!SceneManager.GetSceneByName("UIScene").isLoaded)
            SceneManager.LoadScene("UIScene",LoadSceneMode.Additive);
        if(SceneManager.GetSceneByName(currentSceneName).isLoaded)
            return;
        else
            SceneManager.LoadScene(currentSceneName,LoadSceneMode.Additive);
        
        EventManager.Instance.AddEventListener("ReturnToGame",WhenGameReStart);
        EventManager.Instance.AddEventListener("PauseGame",WhenGamePause);
    }
    // Update is called once per frame
    void Update()
    {

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
    public void BeforeFarmGameStart()
    {
        //切换场景
        //读取关卡数据
    }
    public void AfterFarmGameEnd()
    {

    }
    public GameSaveData GenerateSaveData()
    {
        GameSaveData newGameSaveData = new GameSaveData();
        return newGameSaveData;
    }

    public void RestoreGameSaveData(GameSaveData data)
    {

    }
}
