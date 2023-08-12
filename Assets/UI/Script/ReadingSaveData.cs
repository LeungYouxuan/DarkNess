using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class ReadingSaveData : ButtonClickEvent
{
    // Start is called before the first frame update
    public GameObject saveDataFrame;
    protected override void Start()
    {
        base.Start();  
        //GetComponent<Button>().onClick.AddListener(ButtonClick);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void ButtonClick()
    {
        if(File.Exists(LoaderManager.Instance.jsonFloder+"data.sav"))
        {
            //saveDataFrame.SetActive(true);
            //LoaderManager.Instance.Load();
            LoaderManager.Instance.Load();
        }               
    }
}
