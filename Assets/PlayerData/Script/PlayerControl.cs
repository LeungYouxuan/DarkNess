using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerControl : Singleton<PlayerControl>,ISaveable
{  
    public Sprite playerFace;
    public string playerName="阿杰";
    public bool canOperate;
    public GameObject frame;
    public Text text;
    [SerializeField]
    private float speed=1;

    public bool isTransiton;

    private Rigidbody2D rbody;

    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
    }
    
    private void OnEnable() 
    {
     
    }
    void Start()
    {
        ISaveable saveable=this;
        saveable.SaveableRegister();
        rbody=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        canOperate=true;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            LoaderManager.Instance.Save();
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            LoaderManager.Instance.Load();
        }
        if(isTransiton)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                TransitionManager.Instance.SceneJump(TransitionManager.Instance.currentScene,"AnotherScene");
            }
        }
    }
    void FixedUpdate() 
    {
        if(canOperate)
            Moving();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name=="TransitionPoint")
        {
            Debug.Log("进入传送点附近");
            isTransiton=true;
        }
    }
    void Moving()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical"); 
        rbody.velocity=new Vector2(moveX*speed,moveY*speed);
        if(rbody.velocity.sqrMagnitude!=0)
        {
            if(rbody.velocity.x<0)
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

    public GameSaveData GenerateSaveData()
    {
        GameSaveData newGameSaveData =new GameSaveData();
        newGameSaveData.playerName=this.playerName;
        newGameSaveData.canOperate=this.canOperate;
        return newGameSaveData;
    }

    public void RestoreGameSaveData(GameSaveData data)
    {
        playerName=data.playerName;
        canOperate=data.canOperate;
    }
}
