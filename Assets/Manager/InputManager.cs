using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public Dictionary<string,KeyCode>responseButtonDic=new Dictionary<string, KeyCode>();//按键配置表

    public ResponseKey defaultResponseKeyList;
    
    protected override void Awake()
    {
        base.Awake();
        //这里做一些配置操作
        //UI配置
        responseButtonDic.Add("PauseMenu",KeyCode.Space);//暂停菜单
        responseButtonDic.Add("PlayerMainMenu",KeyCode.Tab);//玩家主菜单
        responseButtonDic.Add("PlayerInfoMenu",KeyCode.B);//玩家背包        
    }
    private void Start() 
    {

    }
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            ReverseKey();
        }
    }
    //改键
    public void ChangeKey(string sourceKey,string targetKey)
    {

    }
    //恢复默认设置
    public void ReverseKey()
    {
        Debug.Log("旧的按键映射"+responseButtonDic["PauseMenu"]);
        responseButtonDic["PauseMenu"]=defaultResponseKeyList.defaultResponseKey[0];
        Debug.Log("新的按键映射:"+responseButtonDic["PauseMenu"]);
    }
}
