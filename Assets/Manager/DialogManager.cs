using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : Singleton<DialogManager>
{
    public bool cancelTyping;

    public bool typeFinshed;

    public DialogFrame dialogFrame;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        typeFinshed=true;  
    }
    private void Update() 
    {
        if(dialogFrame==null||dialogFrame.gameObject.activeSelf==false)
        {
            PlayerControl.Instance.canOperate=true;
        }
        else
        {
            PlayerControl.Instance.canOperate=false;
        }
    }
    /// <summary>
    /// 显示对话框
    /// </summary>
    /// <param name="content   对话内容"></param>    
    /// <param name="face      说话角色的头像"></param>
    /// <param name="name      说话角色的名字"></param>
    /// <param name="lineIndex     行数"></param>
    public int ShowDialog(TextAsset content,Sprite face,string name,int lineIndex)
    {
        //第一次加载
        if(UIManager.Instance.uiStack.Peek().name!="DialogFrame")
            UIManager.Instance.AddUiPanel("DialogFrame");
        else
            UIManager.Instance.uiStack.Peek();
            dialogFrame=UIManager.Instance.uiStack.Peek().GetComponent<DialogFrame>();
            UIManager.Instance.uiStack.Peek().GetComponent<DialogFrame>().characterFace.sprite=face;
            UIManager.Instance.uiStack.Peek().GetComponent<DialogFrame>().characterName.text=name;
        Debug.Log("信息传输完毕");
        //对话按换行符号切割成字符串数组
        var lineContent=content.text.Split('\n');

        return ShowContent(lineContent,0.2f,lineIndex,face,name);
    }   
    public int ShowContent(string[] content,float seconds,int index,Sprite face,string name)
    {       
        dialogFrame.content.text="";//每次显示内容时先清空上一次显示的
        //如果index>=字符数组的长度，也就是说明读完了
        if(index>=content.Length)
        {           
            UIManager.Instance.CloseUiPanel("DialogFrame");
            index=0;
            return index;
        }
        index=index%content.Length;
        if(content[index]=="Npc"+'\r')
        {
            dialogFrame.characterFace.sprite=face;
            dialogFrame.characterName.text=name;
            index++;
        }
        if(content[index]=="Player"+'\r')
        {
            dialogFrame.characterFace.sprite=PlayerControl.Instance.playerFace;
            dialogFrame.characterName.text=PlayerControl.Instance.playerName;
            index++;
        }
        StartCoroutine(PlayContentSpeedControl(content[index],seconds));
        index++;//每按一次，翻页一次
        return index;
    }

    IEnumerator PlayContentSpeedControl(string content,float seconds)
    {
        typeFinshed=false;
        int letter=0;
        while(!cancelTyping&&letter<content.Length-1)
        {
            dialogFrame.content.text+=content[letter];
            letter++;
            yield return new WaitForSeconds(seconds);
        }
        dialogFrame.content.text=content;
        typeFinshed=true;
        cancelTyping=false;
    }
    public void ShowOption()
    {
        
    }
}
