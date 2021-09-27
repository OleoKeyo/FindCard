using System.Collections.Generic;
using FindCard.Infrastructure.Random;
using FindCard.StaticData.Cards;
using UnityEngine;

namespace FindCard.GameLogic.CellLogic
{
  public class CardSequence
  {
    private readonly IRandomService _random;
     
    private readonly CardData[] _sequence;
    private int _lastCard;
    
    public CardSequence(IRandomService random, IReadOnlyList<CardData> cardSet)
    {
      _random = random;
      _sequence = new CardData[cardSet.Count];

      for (int index = 0; index < _sequence.Length; index++)
      {
        _sequence[index] = cardSet[index];
      }
    }

    public void Reset() => 
      _lastCard = 0;
    
    public CardData GetCard()
    {
      int index = GenerateRandomIndex(_lastCard, _sequence.Length);
      if (index >= _sequence.Length)
      {
        return ReturnFirstCard();
      }
      
      CardData card = _sequence[index];
      SwapCard(index, _lastCard);

      return card;
    }
    
    public void RemoveCard(CardData card)
    {
      for (int index = 0; index < _sequence.Length; index++)
      {
        if (_sequence[index].description == card.description)
        {
          SwapCard(index, _lastCard);
          break;
        }
      }
    }

    private int GenerateRandomIndex(int min, int max) =>
      _random.Next(min, max);
    
    private void SwapCard(int first, int second)
    {
      CardData tempCard = _sequence[first];
      _sequence[first] = _sequence[second];
      _sequence[second] = tempCard;
      _lastCard++;
    }
    
    private CardData ReturnFirstCard()
    {
      string message = "Index of randomCard was out of cardSet's range. Return first card from cardSet";
      Debug.Log(message);
      return _sequence[0];
    }
  }
}