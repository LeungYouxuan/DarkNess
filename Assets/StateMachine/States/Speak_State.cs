using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Speak_State : IState
{
    public StateMachine stateMachine;
    public  Speak_State(StateMachine stateMachine)
    {
        this.stateMachine=stateMachine;
    }    
    public void OnEnter()
    {
        Debug.Log("进入说话状态");
    }

    public void OnExit()
    {
        Debug.Log("退出说话状态");
        stateMachine.currentState=stateMachine.stateDic["Walk"];
    }

    public void OnUpdate()
    {
        Debug.Log("执行说话状态");
    }
}
