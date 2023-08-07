using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : ItemOnWorld,ISaveable
{


    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        rbody=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameSaveData GenerateSaveData()
    {
        GameSaveData data =new GameSaveData();
        data.canInteract=canInteract;
        data.itemName=itemName;
        return data;
    }

    public void RestoreGameSaveData(GameSaveData data)
    {
        itemName=data.itemName;
        canInteract=data.canInteract;
    }
    protected override void Interaction()
    {
        
    } 
    private void OnTriggerEnter2D(Collider2D other) 
    {
        PlayerControl player=other.GetComponent<PlayerControl>();
        if(player!=null)
        {
            animator.SetBool("isBroken",true);
        }
    }   
}
