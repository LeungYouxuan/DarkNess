using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Poker : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            StartPokerGame();
        if (Input.GetKeyDown(KeyCode.K))
            EndGame();

    }
    public void StartPokerGame()
    {
        
        PokerManager.Instance.InitalizePokerCard();
        PokerManager.Instance.Shuffle();

    }
    public void EndGame()
    {
        PokerManager.Instance.ReversePokerCard();
    }
}
