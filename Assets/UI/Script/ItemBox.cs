using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class ItemBox : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerMoveHandler
{
    // Start is called before the first frame update
    public Text count;

    public Image itemSprite;

    public int id;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("当前鼠标悬停在第"+id+"个格子");
        //要判断当前格子是否存在物品，那就检查ItemManager的realItemList即可
        if(ItemManager.Instance.realItemList.Count-1>=id)
        {
            
            Vector2 outPos=Input.mousePosition;
            var form=ItemManager.Instance.realItemList[id].GetComponent<PlayerItem>().ReportItemInfo();
            form.gameObject.GetComponent<RectTransform>().SetParent(UIManager.Instance.GetComponent<RectTransform>());
            if(RectTransformUtility.ScreenPointToLocalPointInRectangle(UIManager.Instance.canvas.GetComponent<RectTransform>(),
            Input.mousePosition, null, out outPos))
            {
                Debug.Log("当前UI位置"+outPos);
                Debug.Log("当前鼠标位置"+Input.mousePosition);
                outPos+=new Vector2(200,-300);
                form.GetComponent<RectTransform>().anchoredPosition=outPos;
            }
        }    
        else
        {
            Debug.Log("当前背包格子为空");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //退出悬停时把物品归还到对象池
        Destroy(GameObject.Find("ItemInfoForm"));
        Debug.Log(gameObject.name+"退出检测");
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        GameObject go = GameObject.Find("ItemInfoForm");
        if(go!=null)
        {
            Vector2 outPos;
            if(RectTransformUtility.ScreenPointToLocalPointInRectangle(UIManager.Instance.canvas.GetComponent<RectTransform>(),
            Input.mousePosition, null, out outPos))           
            go.GetComponent<RectTransform>().anchoredPosition=outPos+=new Vector2(200,-300);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
