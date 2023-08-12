using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemOnWorld : MonoBehaviour
{
    public string itemName;

    public bool canInteract;

    public Animator animator;

    public Rigidbody2D rbody;

    //与物品互动
    protected virtual void Interaction()
    {
        
    }

}
