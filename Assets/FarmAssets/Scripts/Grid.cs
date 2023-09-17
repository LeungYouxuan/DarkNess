using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid : ItemOnWorld
{
    public int id;
    public GameObject thisGridSeed;
    private void OnEnable() 
    {
      
    }
    void Start()
    {
        if(GridManager.Instance!=null)
        {
            //GridManager.Instance.gridList.Add(this.gameObject);
            if(!GridManager.Instance.gridPositonDic.ContainsKey(this.gameObject.transform.localPosition))
                GridManager.Instance.gridPositonDic.Add(this.gameObject.transform.localPosition, this.gameObject);
            else
                GridManager.Instance.gridPositonDic[this.gameObject.transform.localPosition]=this.gameObject;
            canInteract=true;
            Debug.Log(this.gameObject.transform.localPosition);
            gameObject.GetComponent<SpriteRenderer>().color=new Color32(79,23,113,255);
        }
        if(transform.localPosition==new Vector3(0,0,0))
        {
            Destroy(gameObject);
        }  
    }

    // Update is called once per frame
    void Update()
    {
        //如果耕地被种下了种子，就马上开始种子生长计时
        if(canInteract==false&&thisGridSeed!=null)
        {
            if(thisGridSeed.GetComponent<Seeds>().thisSeedsData.isRipe==false)
                StartCoroutine(thisGridSeed.GetComponent<Seeds>().StartGrow());
        }
    }
    public override void Interaction()
    {
        if(canInteract&&GridManager.Instance.currentSeed!=null)
        {
            Debug.Log("种菜!");
            //在种子箱的队头取出种子,挂载在耕地格子下
            thisGridSeed=Instantiate(GridManager.Instance.currentSeed);
            thisGridSeed.transform.parent=transform;
            thisGridSeed.transform.position=transform.position;
            canInteract=false;
            CursorManager.Instance.canClick=true;
            GridManager.Instance.AfterPlant();
        }
        else
        {
            CursorManager.Instance.canClick=true;
        }
    }
}
