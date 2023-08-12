using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : NpcBase
{
    // Start is called before the first frame update
    Rigidbody2D rbody2D;
    Animator animator;
    [SerializeField]
    private GameObject point1;
    [SerializeField]
    private GameObject point2;
    private Transform currentPoint;
    public bool canOperate;
    float speed;
    private void Awake() {

    }
    void Start()
    {
        rbody2D=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        speed=2f;
        currentPoint=point2.transform;
        canOperate=true;
    }

    // Update is called once per frame
    private void Update() {
        
    }
    void FixedUpdate()
    {
        if(canOperate)
            RandomMove();
    }
    void RandomMove()
    {
        //巡逻移动
        if(currentPoint==point2.transform)
        {
            //Debug.Log("正向移动");
            rbody2D.velocity=new Vector2(speed,0);
        }
        else if(currentPoint==point1.transform)
        {
            rbody2D.velocity=new Vector2(-speed,0);
        }
        if(Vector2.Distance(transform.position,currentPoint.position)<1.1f&&currentPoint==point2.transform)
        {
           // Debug.Log("距离="+Vector2.Distance(transform.position,currentPoint.position)+"应该要反向移动");
            StartCoroutine(WaitAndSpeek());
            currentPoint=point1.transform;
        }
        if(Vector2.Distance(transform.position,currentPoint.position)<1.1f&&currentPoint==point1.transform)
        {
           // Debug.Log("距离="+Vector2.Distance(transform.position,currentPoint.position)+"应该要正向移动");
            StartCoroutine(WaitAndSpeek());
            currentPoint=point2.transform;
        }
        //动画播放
        if(rbody2D.velocity.sqrMagnitude!=0)
        {
            if(rbody2D.velocity.x<0)
            {
                gameObject.transform.localRotation=Quaternion.Euler(0,180,0);
            }
            else
            {
                gameObject.transform.localRotation=Quaternion.Euler(0,0,0);
            }
            animator.SetBool("isRun",true);
        }
        else
        {
            animator.SetBool("isRun",false);
        }
    }
    public IEnumerator WaitAndSpeek()
    {
        rbody2D.velocity=Vector2.zero;
        canOperate=false;
        yield return new WaitForSeconds(4f);
        canOperate=true;
    }

    // public GameSaveData GenerateSaveData()
    // {
    //     GameSaveData newData =new GameSaveData();
    //     newData.speed=speed;
    //     newData.canOperate=canOperate;
    //     return newData;
    // }

    // public void RestoreGameSaveData(GameSaveData data)
    // {
    //     canOperate=data.canOperate;
    //     speed=data.speed;
    // }
}

