using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class NewGameClick : ButtonClickEvent
{
    // Start is called before the first frame update
    public GameObject selectFrame;

    public GameObject yesButton;

    public GameObject noButton;
    protected override void Start()
    {
        //GetComponent<Button>().onClick.AddListener(ButtonClick);
        base.Start();
        //如果选择是，那么就执行删档操作
        yesButton.GetComponent<Button>().onClick.AddListener(StartNewGame);
        //如果选择否，就关闭选择窗口
        noButton.GetComponent<Button>().onClick.AddListener(CloseFrame);              
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void ButtonClick()
    {
        //GameManager.Instance.SceneJump("StartScene","MainScene");
        //先提示玩家这步操作是开新游戏
        if(File.Exists(LoaderManager.Instance.jsonFloder+"data.sav"))
        {
            selectFrame.SetActive(true);
            Debug.Log(LoaderManager.Instance.jsonFloder);
        }
        else
        {
            
            EventManager.Instance.TriggerEventListener("NewGameStart");//这里后期可以换成用事件管理器来调用Trigger函数
        }
    }
    public void CloseFrame()
    {
        selectFrame.SetActive(false);
    }
    public void StartNewGame()
    {
        selectFrame.SetActive(false);
        EventManager.Instance.TriggerEventListener("NewGameStart");
    }
}
