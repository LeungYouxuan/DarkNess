using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Suit : MonoBehaviour
{
    public SuitData_SO thisSuitData;

    public SuitData_SO templateSuitData;

    private Image thisImage;

    private void Awake()
    {
        if(templateSuitData != null)
        {
            thisSuitData=Instantiate(templateSuitData);
        }
    }

    void Start()
    {
        thisImage = GetComponent<Image>();
        thisImage.sprite = thisSuitData.suitSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
