using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerControl : Singleton<PlayerControl>,ISaveable
{
    public List<Suit> suitList;   
    [SerializeField]
    private TextAsset playerInfo;
    public Sprite playerFace;
    public string playerName="阿杰";
    public bool canOperate=true;

    [SerializeField]
    private float speed=1;

    private Rigidbody2D rbody;

    private Animator animator;

    void Start()
    {
        rbody=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
 
    }
    protected override void Awake()
    {
        base.Awake();
        ISaveable saveable =this;
        saveable.SaveableRegister();
    }
    private void OnEnable() 
    {
     
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
        if(Input.GetKeyDown(KeyCode.F5))
        {
            ScreenCapture.CaptureScreenshot(Application.persistentDataPath+"/ScreenShot.png",0);
        }

    }
    void FixedUpdate() 
    {
        if(canOperate)
            Moving();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Transition")
        {
            LoaderManager.Instance.Save();
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
