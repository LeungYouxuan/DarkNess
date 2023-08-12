using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : Singleton<DialogManager>
{
    public GameObject dialogFrame;//显示的对话框

    public Text dialogText;//显示的内容

    public Image characterFace;//显示的人物头像

    public Text characterName;

    public bool cancelTyping;

    public bool typeFinshed;

    protected override void Awake()
    {
        base.Awake();
        typeFinshed=true;
    }
    private void Update() 
    {
        if(dialogFrame.activeSelf)
        {
            PlayerControl.Instance.canOperate=false;
        }
        else
        {
            PlayerControl.Instance.canOperate=true;
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
        characterFace.sprite = face;

        characterName.text=name;

        dialogFrame.SetActive(true);

        //对话按换行符号切割成字符串数组
        var lineContent=content.text.Split('\n');

        return ShowContent(lineContent,0.2f,lineIndex,face,name);
    }   
    public void CloseDialog()
    {
        dialogFrame.SetActive(false);
    } 
    public int ShowContent(string[] content,float seconds,int index,Sprite face,string name)
    {       
        dialogText.text="";//每次显示内容时先清空上一次显示的
        //如果index>=字符数组的长度，也就是说明读完了
        if(index>=content.Length)
        {           
            dialogFrame.SetActive(false);
            index=0;
            return index;
        }
        index=index%content.Length;
        if(content[index]=="Npc"+'\r')
        {
            characterFace.sprite=face;
            characterName.text=name;
            index++;
        }
        if(content[index]=="Player"+'\r')
        {
            characterFace.sprite=PlayerControl.Instance.playerFace;
            characterName.text=PlayerControl.Instance.playerName;
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
            dialogText.text+=content[letter];
            letter++;
            yield return new WaitForSeconds(seconds);
        }
        dialogText.text=content;
        typeFinshed=true;
        cancelTyping=false;
    }
}
