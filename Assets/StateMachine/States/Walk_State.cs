using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk_State : IState
{
    public StateMachine stateMachine;
    public  Walk_State(StateMachine stateMachine)
    {
        this.stateMachine=stateMachine;
    }
    
    public void OnEnter()
    {
        Debug.Log("进入走路状态");
    }

    public void OnExit()
    {
        Debug.Log("退出走路状态");
    }

    public void OnUpdate()
    {
        //游戏逻辑
        Debug.Log("执行走路状态");
    }
}
