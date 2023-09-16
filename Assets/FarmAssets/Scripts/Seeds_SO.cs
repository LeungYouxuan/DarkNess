using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New SeedData",menuName ="Seeds/Data")]
public class Seeds_SO : ScriptableObject
{
    public string type;
    public string fruitName;
    public float ripeningTime;
    public Sprite seedSprite;
    public int clearCount;
    public bool isRipe;
}
