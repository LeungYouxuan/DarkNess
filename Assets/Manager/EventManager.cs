using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventManager : Singleton<EventManager>
{
    public Dictionary<string,UnityAction>acitonDic=new Dictionary<string, UnityAction>();


    protected override void Awake() {
        base.Awake();
    }
    
    //添加无参事件
    public void AddEventListener(string name,UnityAction action)
    {
        if(acitonDic.ContainsKey(name))
        {
            acitonDic[name]+=action;
        }
        else
        {
            acitonDic.Add(name, action);
        }
    }
    //移除无参事件
    public void RemoveEventListener(string name,UnityAction action)
    {
        if(acitonDic.ContainsKey(name))
        {
            acitonDic[name]-=action;
        }
        else
        {
            
        }
    }
    //触发无参事件
    public void TriggerEventListener(string name)
    {
        if(acitonDic.ContainsKey(name))
        {
            acitonDic[name]?.Invoke();
        }
    }
    //清空事件
    public void CleanEventListener()
    {
        acitonDic.Clear();
    }
}
