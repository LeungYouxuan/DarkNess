using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainMenu : UIPanel
{
    // Start is called before the first frame update
    public KeyCode responseKey=KeyCode.Tab;
    protected override void Start()
    {
        level=0;
        canCover=true;
        GetComponent<RectTransform>().localPosition=new Vector3(0,0,0);
        GetComponent<RectTransform>().sizeDelta=new Vector2(296,161);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
