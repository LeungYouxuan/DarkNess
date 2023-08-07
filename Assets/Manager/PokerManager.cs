using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerManager : Singleton<PokerManager>
{
    // Start is called before the first frame update
    public int cardValue=0;
    public int cardType=0;
    public Sprite[] pokerCardSprite;
    
    public GameObject[] pokerCards;
    public GameObject pokerCardPrefab;
    void Start()
    {
        pokerCards = new GameObject[54];
    }
    void Update()
    {
        
    }
    public void Shuffle()
    {
        Debug.Log("��ʼϴ��");
        //����ʹ��Fisher-Yatesϴ���㷨        
        for (int i = 53; i > 0; i--)
        {
            int randomPos = Random.Range(0, i + 1);
            GameObject temp = pokerCards[i];
            pokerCards[i] = pokerCards[randomPos];
            pokerCards[randomPos] = temp;
        }
        for(int i=0;i<54;i++)
        {
            Debug.Log(pokerCards[i].GetComponent<PokerCard>().Report());
        }
        Debug.Log("����ϴ��");
    }
    public void InitalizePokerCard()
    {
        //ÿ���Ƹ�������        
        for (int i = 0; i < 54; i++)
        {
            cardValue = i % 13 + 1;
            cardType = i / 13;
            pokerCards[i] = ObjectPoolManager.Instance.GetInstance("Poker", pokerCardPrefab);
            pokerCards[i].GetComponent<PokerCard>().MakeCard();
            pokerCards[i].GetComponent<SpriteRenderer>().sprite = pokerCardSprite[i];
        }
        Debug.Log("����˿����������ˣ�");
    }
    public void ReversePokerCard()
    {
        for(int i=0;i<54;i++)
        {
            ObjectPoolManager.Instance.ReturnObject("Poker", pokerCards[i]);
        }
    }
}
