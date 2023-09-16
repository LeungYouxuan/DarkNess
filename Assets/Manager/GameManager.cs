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
        if(SceneManager.GetSceneByName(currentSceneName).isLoaded)
            return;
        else
            SceneManager.LoadScene(currentSceneName,LoadSceneMode.Additive);
        EventManager.Instance.AddEventListener("ReturnToGame",WhenGameReStart);
        EventManager.Instance.AddEventListener("PauseGame",WhenGamePause);

        //注册风车农场游戏的一些事件:
        // EventManager.Instance.AddEventListener("AfterPlant",GridManager.Instance.AfterPlant);
        // EventManager.Instance.AddEventListener("AfterPlant",()=>CursorManager.Instance.canClick=true);
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
    public GameSaveData GenerateSaveData()
    {
        GameSaveData newGameSaveData = new GameSaveData();
        return newGameSaveData;
    }

    public void RestoreGameSaveData(GameSaveData data)
    {

    }
}
