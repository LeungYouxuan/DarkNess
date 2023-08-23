using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultipleBranchSystem;
using ClassLibrary1;
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
    }

    // Update is called once per frame
    void Update()
    {
        if(isDialoging==true&&Input.GetKeyDown(KeyCode.R))
        {
            if(DialogManager.Instance.typeFinshed&&!DialogManager.Instance.cancelTyping)
            {
                index=DialogManager.Instance.ShowDialog(textAssetList[lineIndex],face,npcName,index);
            }
            else if(!DialogManager.Instance.typeFinshed)
            {
                DialogManager.Instance.cancelTyping=true;
            }
        }

    }   
    private void OnTriggerEnter2D(Collider2D other) {
        isDialoging=true;
    }
    private void OnTriggerExit2D(Collider2D other) {
        isDialoging=false;
    }
}
