using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerCard : MonoBehaviour
{
    public int value;
    public int type;
    public Sprite pokerCardSprite;

    private void Awake()
    {
        
    }
    public void MakeCard()
    {
        value = PokerManager.Instance.cardValue;

        type= PokerManager.Instance.cardType;
                
        Report();
    }
    public string Report()
    {
        return this.value.ToString() + "||" + this.type;
    }
}
