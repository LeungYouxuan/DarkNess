using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager : Singleton<MenuManager>
{
    public bool isActive;

    public GameObject pauseMenu;

    public GameObject optionMenu;

    public GameObject mapMenu;
    protected override void Awake() 
    {
        base.Awake();
        
    }
    private void Start() 
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isActive=!isActive;
            pauseMenu.SetActive(isActive);
            if(isActive)
            {
                EventManager.Instance.TriggerEventListener("PauseGame");
                
            }
            else
            {
                EventManager.Instance.TriggerEventListener("ReturnToGame");
            }
            //EventManager.Instance.TriggerEventListener("ReturnToGame");
        }
    }
}
