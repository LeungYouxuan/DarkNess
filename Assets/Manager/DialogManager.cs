using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MultipleBranchSystem;
public class DialogManager : Singleton<DialogManager>
{
    public DialogInstance currentDialogInstance;
    public Dictionary<string,List<DialogInstance>>dialogInstancesDic=new Dictionary<string, List<DialogInstance>>();//对话存储字典
    protected override void Awake()
    {
        base.Awake(); 
    }
    private void Start() 
    {

    }
    public void StartDialoging()
    {
        //第一次加载对话框
        if(UIManager.Instance.uiStack.Count==0)
        {
            UIManager.Instance.AddUiPanel("DialogBox");
        }
        
    }
}
