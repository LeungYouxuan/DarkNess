using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    private Queue<GameObject> menuQueue;

    public GameObject pauseMenu;

    public GameObject optionMenu;

    public GameObject mapMenu;
    protected override void Awake() 
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    private void Start() 
    {

    }
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.M)&&menuQueue.Peek().name!=mapMenu.name)
        {
            menuQueue.Enqueue(mapMenu);
        }

    }

}
