using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

public class Cardgame : MonoBehaviour
{
    public int pairCount;
    public GameObject cardPrefab;
    public Transform cardParent;
    public List<card> cards = new List<card>();
    public List<Sprite> sprites = new List<Sprite>();
    public card firstCard = null;
    public card secondCard = null;
    private bool isChecking = false;

    void Start()
    {
        GenerateCards();
        StartGame();
    }
    void StartGame()
    {
        SoundManager.Instance.PlaySoundBGM();

        List<int> pairNumbers = GeneratePairNumbers(cards.Count);

        for (int i = 0; i < cards.Count; ++i)
        {
            cards[i].SetCardNumber(pairNumbers[i]);

            cards[i].Setimage(sprites[pairNumbers[i]]);
        }


        for (int i = 0; i < cards.Count; ++i)
        {
            cards[i].isFront = false;
        }
    }
    void CheckCard()
    {
        isChecking = true;

        if (firstCard.number == secondCard.number)
        {
            firstCard.ChangeColor(Color.red);
            secondCard.ChangeColor(Color.red);

            firstCard.ismatched = true;
            secondCard.ismatched = true;

            firstCard = null;
            secondCard = null;

            isChecking = false;
        }
        else
        {
            Invoke("HideCard", 1.0f);
        }
    }
    public void OnClickCard(card card)
    {
        if (isChecking)
        {
            return;
        }

        if (firstCard == null)
        {
            firstCard = card;
            firstCard.Flip(true);
            SoundManager.Instance.PlaySoundFx();
        }
        else
        {
            secondCard = card;
            secondCard.Flip(true);
        }

        if (firstCard != null && secondCard != null)
        {
            isChecking = true;
            Invoke("CheckCard", 0.5f);
        }

    }
    void HideCard()
    {
        firstCard.isFront = false;
        secondCard.isFront = false;

        firstCard.Flip(false);
        secondCard.Flip(false);

        isChecking = false;

        firstCard = null;
        secondCard = null;
    }
    List<int> GeneratePairNumbers(int cardCount)
    {
        int pairCount = cardCount / 2;
        List<int> newCardNumbers = new List<int>();

        for (int i = 0; i < pairCount; ++i)
        {
            newCardNumbers.Add(i);
            newCardNumbers.Add(i);
        }


        for (int i = newCardNumbers.Count - 1; i > 0; i--)
        {

            int rnd = Random.Range(0, i + 1);
            int temp = newCardNumbers[i];
            newCardNumbers[i] = newCardNumbers[rnd];
            newCardNumbers[rnd] = temp;
        }

        return newCardNumbers;
    }
    void GenerateCards()
    {
        int totalCard = pairCount * 2;

        for (int i = 0; i < totalCard; i++)
        {
            GameObject obj = Instantiate(cardPrefab, cardParent);
            card newCard = obj.GetComponent<card>();

            newCard.cardGame = this;

            cards.Add(newCard);
        }
    }
}
