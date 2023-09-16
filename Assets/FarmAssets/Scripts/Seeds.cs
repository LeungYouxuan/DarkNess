using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Seeds : MonoBehaviour
{
    // Start is called before the first frame update
    public Seeds_SO templateSeedData;//模版数据
    public Seeds_SO thisSeedsData;
    void Awake()
    {
        if(thisSeedsData == null)
        {
            Debug.Log("获取种子数据");
            thisSeedsData=Instantiate(templateSeedData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //生长计时函数
    public IEnumerator StartGrow()
    {
        thisSeedsData.ripeningTime-=Time.deltaTime;
        yield return new WaitForEndOfFrame();
        if(thisSeedsData.ripeningTime<=0)
        {
            AfterRipe();
        }
    }
    //成熟之后
    public void AfterRipe()
    {
        Debug.Log("成熟了！");
        //做一些成熟之后的操作
        thisSeedsData.isRipe=true;
        //找到父类耕地传入GridManager进行检测销毁
        GridManager.Instance.HarvestCrop(this.gameObject.transform.parent.gameObject);

    }
}
