using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Data",menuName ="PlayerData/Data")]
public class PlayerData_SO : ScriptableObject
{
    public int currentHealth;

    public int maxHealth;

    public float moveSpeed;

    public float maxMoveSpeed;

    public float exp;

    public float maxExp;

    public float physicalPower;

    public float maxPhysicalPower;
}
