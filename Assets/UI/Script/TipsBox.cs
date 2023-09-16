using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TipsBox : UIPanel,IPointerClickHandler
{
    // Start is called before the first frame update
    public Text content;
    public List<GameObject>optionList;
    public void OnPointerClick(PointerEventData eventData)
    {
        //点击提示框就销毁
        UIManager.Instance.PopUIPanel();
        CursorManager.Instance.canClick=true;
    }

    protected override void Start()
    {
        base.Start();
        Debug.Log(optionList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
