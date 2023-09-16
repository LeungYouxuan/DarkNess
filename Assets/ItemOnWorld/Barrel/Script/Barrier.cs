using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : ItemOnWorld
{


    // Start is called before the first frame update
    private void Awake() {
    }
    void Start()
    {
        canInteract=true;
        animator=GetComponent<Animator>();
        rbody=GetComponent<Rigidbody2D>();
        //激活后马上把自己注册进ObjectManager的列表中
        ObjectManager.Instance.itemOnWorldList.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(!canInteract)
        {
            Hide();
        }
    }
    public override void Interaction()
    {
        
    } 
    private void OnTriggerEnter2D(Collider2D other) 
    {
        PlayerControl player=other.GetComponent<PlayerControl>();
        if(player!=null)
        {
            animator.SetBool("isBroken",true);
            canInteract=false;
            //被摧毁后视为状态更新,需要写入字典,这里调用的是自身的单次更新
            ObjectManager.Instance.SaveItemSituation(this);
            //这里要考虑到某些物品会有动画，等动画结束之后才能取消激活
            Invoke("Hide",0.5f);
            
        }
    }
    public void Hide()
    {
        gameObject.SetActive(canInteract);
    }
}
