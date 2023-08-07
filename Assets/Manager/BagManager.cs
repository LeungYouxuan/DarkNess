using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : Singleton<BagManager>
{
    // Start is called before the first frame update

    public GameObject suitPanel;
    void Start()
    {       
        CreateSuit();
    }
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateSuit()
    {
        for(int i=0;i<PlayerControl.Instance.suitList.Count;i++)
        {
            Suit newSuit = Instantiate(PlayerControl.Instance.suitList[i],suitPanel.transform.position,Quaternion.identity);
            newSuit.gameObject.transform.SetParent(suitPanel.transform);
        }

    }
}
