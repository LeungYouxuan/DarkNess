using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultipleBranchSystem;
public class Tramp : NpcBase
{
    Tree<TextAsset> dialogTree;


    void Awake()
    {
        dialogTree=new Tree<TextAsset>(textAssetList[0]);
        TreeNode<TextAsset>nodeA=dialogTree.Root;
        dialogTree.AddChild(nodeA,textAssetList[1]);
        dialogTree.AddChild(nodeA,textAssetList[2]);
    }
    void OnEnable() 
    {

    }
    void Start()
    {
        lineIndex=0;
        index=0;
        Debug.Log("当前对话节点的子节点数:"+dialogTree.Root.ChildList.Count);
    }

    // Update is called once per frame
    void Update()
    {

    }   
    public override void Interaction()
    {
        if(isDialoging&&canTalk)
        {
            Debug.Log("对话中");

            if(DialogManager.Instance.typeFinshed&&!DialogManager.Instance.cancelTyping)
            {
                if(face==null)
                    Debug.Log("NULL");
                
                index=DialogManager.Instance.ShowDialog(textAssetList[lineIndex],face,npcName,index);
            }
            else if(!DialogManager.Instance.typeFinshed)
            {
                DialogManager.Instance.cancelTyping=true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        canTalk=true;
    }
    private void OnTriggerExit2D(Collider2D other) {
        canTalk=false;
    }
}
