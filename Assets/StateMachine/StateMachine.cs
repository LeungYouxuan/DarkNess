using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateMachine : MonoBehaviour
{
    
    public Dictionary<string,IState>stateDic;//状态字典,用于存储每个状态

    public Stack<IState>stateStack;//状态栈

    public IState currentState;

    public bool isComplete;

    private bool isRunning;

    void Start()
    {
        stateStack=new Stack<IState>();
        stateDic=new Dictionary<string, IState>();
        stateDic.Add("Walk",new Walk_State(this));
        stateDic.Add("Speak",new Speak_State(this));
        stateStack.Push(stateDic["Walk"]);
    }
    // Update is called once per frame
    void Update()
    {
        //始终保持执行栈顶状态
        if(stateStack.Peek()==null)
        {
            stateStack.Push(stateDic["Walk"]);
        }
        else
        {
            stateStack.Peek().OnUpdate();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            stateStack.Push(stateDic["Speak"]);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            stateStack.Pop();
        }
    }
    //添加状态进入状态字典
    public void AddStateToStateDic(string stateName,IState state)
    {
        if(!stateDic.ContainsKey(stateName))
        {
            stateDic.Add(stateName,state);
        }
        else
        {
            stateDic[stateName]=state;
        }
    }
    //从状态字典移除状态
    public void RemoveStateFromStateDic(string stateName)
    {
        if(stateDic.ContainsKey(stateName))
        {
            stateDic.Remove(stateName);
        }
        else
        {
            return;
        }        
    }
}
