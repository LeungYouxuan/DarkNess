using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class UIPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public bool canCover;//是否能被覆盖

    public int level;
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
