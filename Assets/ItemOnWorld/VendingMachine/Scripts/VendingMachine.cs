using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : ItemOnWorld
{
    private Dictionary<string,GameObject>goodsList;
    
    public GameObject vendingMachineFrame;

    //public GameObject chatFrame;

    public bool isInteract;

    void Start()
    {
        this.isInteract=false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract&&Input.GetKeyDown(KeyCode.R))
        {
            vendingMachineFrame.SetActive(true);
        }
        else if(canInteract==false)
        {
            vendingMachineFrame.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag=="Player")
        {
            Debug.Log("进入检测范围");
            canInteract=true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag=="Player")
        {
            Debug.Log("退出检测范围");
            canInteract=false;
        }        
    }
}
