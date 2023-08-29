using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public PlayerData_SO playerData;

    public PlayerData_SO templatePlayData;
    #region Êý¾Ý¶ÁÈë
    public int CurrentHealth
    {
        get
        {
            if (playerData != null)
                return playerData.currentHealth;
            return 0;
        }
        set
        {
            playerData.currentHealth = value;
        }
    }
    public int MaxHealth
    {
        get
        {
            if (playerData != null)
                return playerData.maxHealth;
            return 0;
        }
        set
        {
            playerData.maxHealth = value;
        }
    }
    public float MoveSpeed
    {
        get
        {
            if (playerData != null)
                return playerData.moveSpeed;
            return 0;
        }
        set
        {
            playerData.moveSpeed = value;
        }
    }
    public float MaxMoveSpeed
    {
        get
        {
            if (playerData != null)
                return playerData.maxMoveSpeed;
            return 0;
        }
        set
        {
            playerData.maxMoveSpeed = value;
        }
    }
    public float Exp
    {
        get
        {
            if (playerData != null)
                return playerData.exp;
            return 0;
        }
        set
        {
            playerData.exp = value;
        }
    }
    public float MaxExp
    {
        get
        {
            if (playerData != null)
                return playerData.maxExp;
            return 0;
        }
        set
        {
            playerData.maxExp = value;
        }
    }
    public float PhysicalPower
    {
        get
        {
            if (playerData != null)
                return playerData.physicalPower;
            return 0;
        }
        set { playerData.physicalPower = value; }
    }
    public float MaxPhysicalPower
    {
        get
        {
            if (playerData != null)
                return playerData.maxPhysicalPower;
            return 0;
        }
        set { playerData.maxPhysicalPower = value; }
    }
    #endregion
    private void Awake()
    {
        if(templatePlayData!=null)
        {
            playerData=Instantiate(templatePlayData);
        }
    }
}
