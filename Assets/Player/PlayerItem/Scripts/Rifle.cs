using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : PlayerItem
{
    // Start is called before the first frame update
    public int damage;

    public int level;

    protected override void Start()
    {
        base.Start();
        damage=26;
        level=1;
        itemName="Rifle";
        itemType="武器-步枪";
        describe="海文重工出产的轻型突击步枪，发射5.56毫米子弹，拥有不俗的精准度和杀伤力";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override GameObject ReportItemInfo()
    {
        GameObject go=Instantiate(ItemInfoForm);
        go.name="ItemInfoForm";
        go.GetComponent<ItemInfoForm>().itemDescrition.text="威力"+damage.ToString()+"\n"+describe;//写入描述
        go.GetComponent<ItemInfoForm>().itemName.text+=itemName;//写入名字
        go.GetComponent<ItemInfoForm>().itemType.text+=itemType;//写入类型
        go.GetComponent<ItemInfoForm>().itemLevel.text+=level.ToString();//写入等级
        go.gameObject.SetActive(true);
        return go;
    }
}
