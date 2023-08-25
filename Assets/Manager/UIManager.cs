using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    public Dictionary<string,GameObject>uiPanelPrefabs=new Dictionary<string, GameObject>();
    public Stack<GameObject>uiStack=new Stack<GameObject>();
    public List<GameObject>uiPanelList;
    public GameObject rootPanel;
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
        
    }
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("打开暂停菜单面板");
            AddUiPanel("PauseMenu");
        }
    }
    public void AddUiPanel(string targetName)
    {
        //如果字典里面存有该UiPanel,再去判断栈顶元素是否能被叠加
        if(uiPanelPrefabs.ContainsKey(targetName))
        {
            Debug.Log("开始尝试打开面板"+targetName);
            if(uiStack.Count==0)
            {
                var ui=Instantiate(uiPanelPrefabs[targetName]);
                ui.gameObject.transform.SetParent(gameObject.transform);
                ui.gameObject.name=targetName;
                uiStack.Push(ui);
                uiStack.Peek().gameObject.SetActive(true);            
            }
            else
            {
                //如果栈顶元素就是当前所要添加的UI，那么就执行关闭操作，否则则进行添加操作
                if(uiStack.Peek().name!=targetName)
                {
                    Debug.Log("开始打开面板"+targetName);
                    Debug.Log(uiStack.Count);
                    Debug.Log(uiStack.Peek().GetComponent<UIPanel>().canCover);
                    Debug.Log(uiStack.Peek().GetComponent<UIPanel>().level);
                    Debug.Log(uiPanelPrefabs[targetName].GetComponent<UIPanel>().level);
                    //如果栈顶元素不能共存运行，那么将按照优先级顺序进行运行，优先级低的将被弹出栈,优先级相等则说明可以共存
                    if(uiStack.Peek().GetComponent<UIPanel>().canCover==false&&
                    uiStack.Peek().GetComponent<UIPanel>().level<uiPanelPrefabs[targetName].GetComponent<UIPanel>().level)
                    {
                        Debug.Log("目标"+targetName+"优先级大于当前栈顶元素,栈顶元素被弹出");
                        //uiStack.Pop();
                        //这里不单独写弹出栈了，直接进行关闭。
                        CloseUiPanel(uiStack.Peek().name);
                        //入栈
                        Debug.Log("把目标"+targetName+"填入栈");
                        var ui=Instantiate(uiPanelPrefabs[targetName]);
                        ui.gameObject.transform.SetParent(gameObject.transform);
                        ui.gameObject.name=targetName;
                        uiStack.Push(ui);
                        uiStack.Peek().gameObject.SetActive(true);                    
                    }
                    else if(uiStack.Peek().GetComponent<UIPanel>().canCover==false&&
                    uiStack.Peek().GetComponent<UIPanel>().level>uiPanelPrefabs[targetName].GetComponent<UIPanel>().level)
                    {
                        Debug.Log("目标"+targetName+"填入栈失败，目标优先级不够高");
                        return;
                    }
                    //考虑到有些UI能被部分UI叠加，部分又不能被叠加，所以要考虑canCover为true时，通过优先级决定去留
                    else if(uiStack.Peek().GetComponent<UIPanel>().canCover&&
                    uiStack.Peek().GetComponent<UIPanel>().level<uiPanelPrefabs[targetName].GetComponent<UIPanel>().level)
                    {
                        //入栈
                        Debug.Log("目标优先级高于当前栈顶元素优先级,当前栈顶元素被弹出");
                        CloseUiPanel(uiStack.Peek().name);
                        Debug.Log("把目标"+targetName+"填入栈");
                        var ui=Instantiate(uiPanelPrefabs[targetName]);
                        ui.gameObject.transform.SetParent(gameObject.transform);
                        ui.gameObject.name=targetName;
                        uiStack.Push(ui);
                        uiStack.Peek().gameObject.SetActive(true);     
                    }
                    else if(uiStack.Peek().GetComponent<UIPanel>().canCover&&
                    uiStack.Peek().GetComponent<UIPanel>().level>=uiPanelPrefabs[targetName].GetComponent<UIPanel>().level)
                    {
                        Debug.Log("把目标"+targetName+"填入栈");
                        var ui=Instantiate(uiPanelPrefabs[targetName]);
                        ui.gameObject.transform.SetParent(gameObject.transform);
                        ui.gameObject.name=targetName;
                        uiStack.Push(ui);
                        uiStack.Peek().gameObject.SetActive(true);                          
                    }
                }
                else if(uiStack.Peek().name==targetName)
                {
                    Debug.Log("关闭"+targetName+"面板");
                    CloseUiPanel(targetName);
                }                
            }
        }
        else
        {
            Debug.Log("UI管理器中没有该UI画板的记录");
            return;
        }
    }
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

}
