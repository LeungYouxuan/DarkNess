using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : UIPanel
{
    // Start is called before the first frame update
    public KeyCode responseKey=KeyCode.Escape;
    protected override void Start()
    {
        level=3;
        canCover=false;
        GetComponent<RectTransform>().sizeDelta=new Vector2(1000,1000);
        GetComponent<RectTransform>().anchoredPosition=new Vector2(0,0);
        GetComponent<RectTransform>().anchorMax=new Vector2(0.5f,0.5f);
        GetComponent<RectTransform>().anchorMin=new Vector2(0.5f,0.5f);
    }

}
