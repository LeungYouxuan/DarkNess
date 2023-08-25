using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>,ISaveable
{
   protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        //ISaveable saveable =this;
        //saveable.SaveableRegister();
        if(SceneManager.GetSceneByName("MainScene").isLoaded==false)
        {
            Debug.Log("主场景未加载，现在正在加载");
            SceneManager.LoadScene("MainScene",LoadSceneMode.Additive);
        }
        Scene loadScene=SceneManager.GetSceneByName("MainScene");
        SceneManager.sceneLoaded+=(Scene sc,LoadSceneMode loadSceneMode)=>
        {
            //SceneManager.SetActiveScene(loadScene);
            //Debug.Log("设置了新的场景为激活状态");
        };
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
    public GameSaveData GenerateSaveData()
    {
        GameSaveData newGameSaveData = new GameSaveData();
        return newGameSaveData;
    }

    public void RestoreGameSaveData(GameSaveData data)
    {

    }

}
