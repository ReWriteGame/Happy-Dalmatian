using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeckMaker : MonoBehaviour
{
    [SerializeField] private List<Card> cards;
    [SerializeField] private Sprite[] suitsCards;

    private bool[] setCards;

    public List<Card> Cards { get => cards; private set => cards = value; }

    public UnityEvent caedsIsOverEvent;

    private void Start()
    {
        setCards = new bool[cards.Count];

        for (int i = 0; i < setCards.Length; i++)
            setCards[i] = false;
        for (int i = 0, numOfSuit = 0; i < cards.Count - cards.Count % 2; i += 2, numOfSuit++)
        {
            setRandom2Suit(numOfSuit % suitsCards.Length);// 
        }
    }

    public void checkCards()
    {
        if (cards.Count <= 0) caedsIsOverEvent?.Invoke();
    }

    private void setRandom2Suit(int numOfSuitCard)
    {
        Sprite sprite = suitsCards[numOfSuitCard];
        int indexOne;
        int indexTwo;

        do
        {
            indexOne = Random.Range(0, cards.Count);
            indexTwo = Random.Range(0, cards.Count);
        } while (indexOne == indexTwo || setCards[indexOne]  || setCards[indexTwo]);

        setCards[indexOne] = true;
        setCards[indexTwo] = true;
        cards[indexOne].suit.sprite = sprite;
        cards[indexTwo].suit.sprite = sprite;
        cards[indexOne].indexSuit = numOfSuitCard;
        cards[indexTwo].indexSuit = numOfSuitCard;
    }
}
