using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class CursorManager : Singleton<CursorManager>
{
    private Vector3 mousPos;

    private bool canClick;

    [SerializeField]
    private Texture2D cursorSprite;

    protected override void Awake() 
    {
        base.Awake();

    }
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            StartCoroutine(ClickObject());
        }
    }
    IEnumerator  ClickObject()
    {
        
        RaycastHit2D hit2D=Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
        if(hit2D.collider!=null&&hit2D.collider.GetComponent<NpcBase>()!=null)
        {
            hit2D.collider.GetComponent<NpcBase>().isDialoging=true;
            if(hit2D.collider.GetComponent<NpcBase>().canTalk)
            {
                Debug.Log("与目标NPC："+hit2D.collider.name+"开始对话");
                hit2D.collider.GetComponent<NpcBase>().Interaction();
                yield return new WaitForEndOfFrame();  
            }
            else
            {
                Debug.Log("未进入交谈范围");
            }
                       
        }     
        if(hit2D.collider==null)
        {
            Debug.Log("点击地面");
        }
    }
}
