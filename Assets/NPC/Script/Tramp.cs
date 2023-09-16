using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultipleBranchSystem;
public class Tramp : NpcBase
{
    [SerializeField]
    private DialogInstance currentNpcDialogInstance;
    private Dictionary<string,DialogInstance>npcDialogInstanceDic=new Dictionary<string, DialogInstance>();//对话内容字典
    void Awake()
    {
        
    }
    void OnEnable() 
    {
    }
    void Start()
    {
        lineIndex=0;
        index=0;
    }
    // Update is called once per frame
    void Update()
    {

    }   
    public override void Interaction()
    {
        //上传对话实例到DialogManager
        DialogManager.Instance.currentDialogInstance=currentNpcDialogInstance;
        //这里可以执行一些操作，用于开始对话前.
        DialogManager.Instance.StartDialoging();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        canTalk=true;
    }
    private void OnTriggerExit2D(Collider2D other) {
        canTalk=false;
    }
}
