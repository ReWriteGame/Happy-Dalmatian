using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameRule : MonoBehaviour
{
    [SerializeField] private DeckMaker deck;
    [SerializeField] private int numCardsPerTurn = 2;
    [SerializeField] private float dalayDestroy = 0.5f;
    [SerializeField] private float dalayHideCards = 0.7f;



    public UnityEvent charactersMatchEvent;
    public UnityEvent charactersNotMatchEvent;

    private List<Card> activeCards;
    private int numberOfCoincidences = 0;



    private void Start()
    {
        charactersMatchEvent.AddListener(destroyMatchCards);
        charactersNotMatchEvent.AddListener(hideAllFlags);
    }
    public void compareActiveCards()
    {
        activeCards = new List<Card>(numCardsPerTurn);

        //for (int n = 0; n < numCardsPerTurn; n++)
            for (int i = 0; i < deck.Cards.Count; i++)
                if (deck.Cards[i].isActive.active)
                    activeCards.Add(deck.Cards[i]);

        print(activeCards.Count);

        if (activeCards.Count == numCardsPerTurn)
            for (int i = 0; i < activeCards.Count - 1; i++)
                if (activeCards[i].indexSuit == activeCards[i + 1].indexSuit)
                    numberOfCoincidences++;

        if (numberOfCoincidences == numCardsPerTurn - 1)
            charactersMatchEvent?.Invoke();
        else if (activeCards.Count >= numCardsPerTurn)
            charactersNotMatchEvent?.Invoke();

        if (activeCards.Count >= numCardsPerTurn)
        {
            numberOfCoincidences = 0;
        }
    }
    private void destroyMatchCards()
    {
        foreach (Card obj in activeCards)
        {
            deck.Cards.Remove(obj);
            obj.GetComponent<Destroyable>().destroy(dalayDestroy);
        }

        deck.checkCards();
    }
 


    public void hideAllFlags()
    {
        StartCoroutine(hideAllFlagsCor());
    }

    private IEnumerator hideAllFlagsCor()
    {
        yield return new WaitForSeconds(dalayHideCards);
        for (int i = 0; i < deck.Cards.Count; i++)
            deck.Cards[i].isActive.SetActive(false);
        yield break;
    }

  
}


