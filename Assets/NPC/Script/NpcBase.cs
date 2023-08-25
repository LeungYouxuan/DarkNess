using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NpcBase : MonoBehaviour
{
    // Start is called before the first frame update
    public List<TextAsset> textAssetList;//对话列表，一个元素存储一个文本

    public string npcName;

    public Sprite face;

    public bool canTalk;

    public bool isDialoging;
    public int lineIndex;//同一个文本，页数下标

    public int index;//对话总数下标

    void Awake()
    {
        
    }
    public virtual void Interaction()
    {
        // if(isDialoging&&canTalk&&Input.GetMouseButtonDown(0))
        // {
        //     Debug.Log("对话中");
        //     if(DialogManager.Instance.typeFinshed&&!DialogManager.Instance.cancelTyping)
        //     {
        //         index=DialogManager.Instance.ShowDialog(textAssetList[lineIndex],face,npcName,index);
        //     }
        //     else if(!DialogManager.Instance.typeFinshed)
        //     {
        //         DialogManager.Instance.cancelTyping=true;
        //     }
        // }
    }
}
