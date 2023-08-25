using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BookShelf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.GetComponent<PlayerControl>()!=null)
        {
            //gameObject.GetComponent<SpriteRenderer>().color=new Color32(255,255,255,40);
            gameObject.GetComponent<SpriteRenderer>().DOColor(new Color32(255,255,255,100),0.5f);
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.GetComponent<PlayerControl>()!=null)
        {
            gameObject.GetComponent<SpriteRenderer>().DOColor(new Color32(255,255,255,255),0.5f);
        }
    }
}
