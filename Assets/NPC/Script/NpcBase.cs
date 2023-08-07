using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NpcBase : MonoBehaviour
{
    // Start is called before the first frame update
    public List<TextAsset> textAssetList;//对话列表，一个元素存储一个文本

    public string npcName;

    public Sprite face;

    public bool isDialoging;
    public int lineIndex;//同一个文本，页数下标

    public int index;//对话总数下标

    void Awake()
    {
        
    }
}
