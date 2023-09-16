using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CashierDesk : ItemOnWorld
{
    // Start is called before the first frame update
    private string showContent="确定要开始今天的营业吗？";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Interaction()
    {
        UIManager.Instance.AddUiPanel("TipsBox");
        if(UIManager.Instance.uiStack.Peek().name=="TipsBox")
        {
            UIManager.Instance.uiStack.Peek().GetComponent<TipsBox>().content.text=showContent;
            UIManager.Instance.uiStack.Peek().GetComponent<TipsBox>().optionList[0].GetComponent<Text>().text="是";
            UIManager.Instance.uiStack.Peek().GetComponent<TipsBox>().optionList[0].GetComponent<Button>().onClick.AddListener(DebugLogT);
            UIManager.Instance.uiStack.Peek().GetComponent<TipsBox>().optionList[1].GetComponent<Text>().text="否";
            UIManager.Instance.uiStack.Peek().GetComponent<TipsBox>().optionList[1].GetComponent<Button>().onClick.AddListener(DebugLogF);
        }
        else
        {
            return;
        }
    } 
    public void DebugLogT()
    {
        Debug.Log(true);
    }
    public void DebugLogF()
    {
        Debug.Log(false);
    }
}
