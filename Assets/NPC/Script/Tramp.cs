using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tramp : NpcBase
{
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
