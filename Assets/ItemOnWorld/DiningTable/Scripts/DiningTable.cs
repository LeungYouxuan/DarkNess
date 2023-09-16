using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiningTable : ItemOnWorld
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Interaction()
    {
        Debug.Log("上菜");
        CursorManager.Instance.canClick=true;
    }
}
