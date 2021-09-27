using System.Collections.Generic;
using FindCard.GameLogic.CellLogic;

namespace FindCard.StaticData.Cards
{
  public class CardSetLibrary
  {
    public IReadOnlyList<CardData> CardSet => _cardSet;

    private readonly List<CardData> _cardSet = new List<CardData>();
    
    public CardSetLibrary(List<CardData> cards)
    {
      foreach (CardData card in cards)
        _cardSet.Add(card);
    }
  }
}