using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class DialogBox : UIPanel
{
    public string[] showContent;
    public Image characterFace;
    public Text showContentText;
    public Text characterNameText;
    public List<Text>optionText;
    public List<GameObject>optionImage;
    int index;
    protected override void Start()
    {
        showContent=DialogManager.Instance.currentDialogInstance.dialogText.text.Split('\n');
        index=0;
        level=0;
        gameObject.GetComponent<RectTransform>().anchoredPosition=new Vector2(38.269f,-311);
        //这里需要初始化第一句话
        UpadateShowContent();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            UpadateShowContent();
        }
    }
    public void UpadateShowContent()
    {
        //说话时先清空上一次说话的内容
        Debug.Log(index);
        showContentText.text="";
        //判断读取的行标是否到达边界
        if(index>=showContent.Length-1)
        {
            /*一段对话的完结，有两种可能性：
            1.对话真的完结了
            2.有选项需要玩家选择
            */
            //具体操作：
            /*如果读取到DialogInstance中的Option列表不为空,那么就要暂时中断对话，执行以下操作：
            step1：通过for循环遍历把所有的Option显示到OptionList中
            step2:注册Option的功能
            */
            Debug.Log("当前对话含有选项个数:"+DialogManager.Instance.currentDialogInstance.optionList.Count);
            if(DialogManager.Instance.currentDialogInstance.optionList.Count!=0)
            {
                Debug.Log("开始显示选项");
                for(int i=0;i<optionImage.Count;i++)
                {
                    optionImage[i].gameObject.SetActive(true);
                    optionText[i].text=DialogManager.Instance.currentDialogInstance.optionList[i].showText;
                    //注册按钮事件:
                    
                    //Debug.Log("选项"+i+DialogManager.Instance.currentDialogInstance.optionList[i].showText);
                    
                }
                index=0;
                return;
            }
            index=0;
            UIManager.Instance.PopUIPanel();
            CursorManager.Instance.canClick=true;
            return;
        }
        //遇到@标识符，则说明这一行这是说话对象的名字(非玩家)，下一行是说话内容
        else if(showContent[index][0]=='@')
        {
            //刷新说话对象名字,头像
            characterNameText.text=showContent[index];
            characterFace.sprite=DialogManager.Instance.currentDialogInstance.characterFace;
            index++;
        }
        else if(showContent[index]=="Player\r")
        {
            
            if(PlayerControl.Instance!=null)
            {
                //刷新说话对象名字,头像
                characterNameText.text=PlayerControl.Instance.playerName;
                characterFace.sprite=PlayerControl.Instance.playerFace;
                //显示说话内容
                index++;
            }
        }
        showContentText.DOText(showContent[index],1f);
        index++; 
    }
}
