using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashroomDoor : ItemOnWorld
{
    // Start is called before the first frame update
    [SerializeField]
    TextAsset text;
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Interaction()
    {
        animator.SetBool("isOpen",true);
        //DialogManager.Instance.ShowDialog(text,PlayerControl.Instance.playerFace,PlayerControl.Instance.playerName,0);
    }
}
