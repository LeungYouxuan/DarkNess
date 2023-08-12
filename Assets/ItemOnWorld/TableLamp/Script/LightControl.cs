using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LightControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Light2D light2DControl;

    void Start()
    {
        light2DControl=GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
