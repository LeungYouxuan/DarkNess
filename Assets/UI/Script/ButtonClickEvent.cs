using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class ButtonClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    protected virtual void Start()
    {
        GetComponent<Button>().onClick.AddListener(ButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected virtual void ButtonClick()
    {
        
    }
}
