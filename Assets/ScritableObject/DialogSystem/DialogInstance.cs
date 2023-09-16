using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName ="New DialogData",menuName ="Dialog/Data")]
public class DialogInstance : ScriptableObject
{
    [SerializeField]
    public TextAsset dialogText;
    public Sprite characterFace;
    public string dialogName;
    public List<DialogOption>optionList;
    
}
