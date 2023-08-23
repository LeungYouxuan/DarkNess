using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class CursorManager : Singleton<CursorManager>
{
    private Vector3 mousPos;

    private bool canClick;

    protected override void Awake() 
    {
        base.Awake();

    }
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ClickObject());
        }
    }
    IEnumerator  ClickObject()
    {
        RaycastHit2D hit2D=Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,LayerMask.GetMask("Door"));
        if(hit2D.collider!=null)
        {
            Debug.Log("Target"+hit2D.collider.name);
            yield return new WaitForEndOfFrame();             
        }     
        if(hit2D.collider==null)
        {
            Debug.Log("点击地面");
        }
    }
}
