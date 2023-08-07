using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Data", menuName = "SuitData/Data")]
public class SuitData_SO : ScriptableObject
{
    public string type;

    public Sprite suitSprite;

    public float health;

    public float speed;

    public float attack;

    public float defense;

    public float physicalPower;

    [TextArea] public string description;
}
