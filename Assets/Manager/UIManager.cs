using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    public Dictionary<string,GameObject>uiPanelPrefabs=new Dictionary<string, GameObject>();
    public Stack<GameObject>uiStack=new Stack<GameObject>();
    public List<GameObject>uiPanelList;//方便外部赋值

    public Dictionary<string,KeyCode>responseButtonDic=new Dictionary<string, KeyCode>();//按键配置表
    public GameObject rootPanel;

    public Canvas canvas;

    protected override void Awake()
    {
        base.Awake();
        foreach(var ui in uiPanelList)
        {
            uiPanelPrefabs.Add(ui.name,ui);
            Debug.Log(uiPanelPrefabs[ui.name].name);
        }
    }
    private void Start() 
    {
        canvas=GetComponent<Canvas>();

    }
    private void Update() 
    {
        if(Input.GetKeyDown(InputManager.Instance.responseButtonDic["PauseMenu"]))
        {
            Debug.Log("打开暂停菜单面板");
            AddUiPanel("PauseMenu");
        }
        if(Input.GetKeyDown(InputManager.Instance.responseButtonDic["PlayerMainMenu"]))
        {
            Debug.Log("打开玩家主要菜单");
            AddUiPanel("PlayerMainMenu");
        }
        if(Input.GetKeyDown(InputManager.Instance.responseButtonDic["PlayerInfoMenu"]))
        {
            Debug.Log("打开玩家信息菜单");
            AddUiPanel("PlayerInfoMenu");
        }        
    }
    public void AddUiPanel(string targetName)
    {
        //如果字典里面存有该UiPanel,再去判断栈顶元素是否能被叠加
        if(uiPanelPrefabs.ContainsKey(targetName))
        {
            if(uiStack.Count==0)
            {
                //如果当前UI栈为空，那么直接填入目标
                PushUIPanel(targetName);
            }
            else
            {
                //如果栈顶元素就是当前所要添加的UI，那么就执行关闭操作，否则则进行添加操作
                if(uiStack.Peek().name!=targetName)
                {
                    //先判断当前栈顶元素UI是否能被覆盖，若能被覆盖，则把栈顶元素弹出栈，若不能，则把目标UI失活
                    //如果不能被覆盖：
                    if(uiStack.Peek().GetComponent<UIPanel>().canCover==false)
                    {
                        Debug.Log("栈顶元素UI："+targetName+"不能被覆盖，无法打开目标UI"); 
                        return;                
                    }
                    //考虑有些UI需要在游戏游玩期间常驻，与其它UI形成共存状态，这里引入等级参数，等级为1的栈顶元素共存，无需出栈
                    //step1：
                    //      能共存且目标UI渲染在栈顶UI之上,直接Push
                    else if(uiStack.Peek().GetComponent<UIPanel>().canCover&&uiStack.Peek().GetComponent<UIPanel>().level==1)
                    {
                        PushUIPanel(targetName);
                    }
                    //step2:
                    //      不能共存，Pop操作接Push操作
                    else if(uiStack.Peek().GetComponent<UIPanel>().canCover&&uiStack.Peek().GetComponent<UIPanel>().level==0)
                    {
                        PopUIPanel();
                        PushUIPanel(targetName);
                    }
                }
                else
                {
                    PopUIPanel();
                    return;
                }              
            }
        }
        else
        {
            Debug.Log("UI管理器中没有该UI画板的记录");
            return;
        }
    }
    //非常规关闭
    public void CloseUiPanel(string targetName)
    {
        //先判断栈顶元素是否为空切是否为当前要关闭的
        if(uiStack.Count!=0)
        {
            Debug.Log("尝试执行弹出操作");
            if(uiStack.Peek().name==targetName)
            {
                var ui=uiStack.Pop();
                ui.gameObject.SetActive(false);
                Debug.Log("目标面板"+targetName+"已被弹出");
                Debug.Log("当前栈顶元素是："+uiStack.Peek().name);
                Destroy(ui.gameObject);
            }
            else
            {
                Debug.Log("当前UI未打开或者无法关闭");
            }        
        }
        else
        {
            Debug.Log("当前栈中元素数量为:"+uiStack.Count);
        }
    }
    private void PushUIPanel(string targetName)
    {
        var ui=Instantiate(uiPanelPrefabs[targetName]);
        ui.gameObject.transform.SetParent(gameObject.transform);
        ui.gameObject.name=targetName;
        uiStack.Push(ui);
        uiStack.Peek().gameObject.SetActive(true);       
    }
    private void PopUIPanel()
    {
        Debug.Log("弹出栈顶UI");
        Destroy(uiStack.Pop());
    }
}
