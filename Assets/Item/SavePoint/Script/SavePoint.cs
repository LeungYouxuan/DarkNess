using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SavePoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SaveFrame;

    public GameObject yesButton;

    public GameObject noButton;

    public TextAsset content;

    public int index;

    public bool isDialog;
    void Start()
    {
        index=0;
        yesButton.GetComponent<Button>().onClick.AddListener(LoaderManager.Instance.Save);
        noButton.GetComponent<Button>().onClick.AddListener(DialogManager.Instance.CloseDialog); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)&&isDialog)
        {
            if(DialogManager.Instance.typeFinshed&&!DialogManager.Instance.cancelTyping)
            {
                index=DialogManager.Instance.ShowDialog(content,PlayerControl.Instance.playerFace,PlayerControl.Instance.playerName,index);
                yesButton.SetActive(true);
                noButton.SetActive(true);
            }    
            else if(!DialogManager.Instance.typeFinshed)
            {
                DialogManager.Instance.cancelTyping=true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        isDialog=true;
        Debug.Log("弹出存档提示框"+isDialog);
    }
    private void OnCollisionExit2D(Collision2D other) {
        isDialog=false;
        Debug.Log("关闭存档提示框"+isDialog);
    }
}
