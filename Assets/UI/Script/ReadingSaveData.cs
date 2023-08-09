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
        //如果存档文件存在，那么就把存档菜单显示出来
        //加载存档的场景
        if(File.Exists(LoaderManager.Instance.jsonFloder+"data.sav"))
        {
            //saveDataFrame.SetActive(true);
            //  
            //LoaderManager.Instance.Load();
            LoaderManager.Instance.Load();
            GameManager.Instance.SceneJump("StartScene",GameManager.Instance.currentScene);
        }               
    }
}
