using System.Collections.Generic;
using FindCard.GameLogic.CellLogic;
using FindCard.StaticData.Cards;
using FindCard.StaticData;

namespace FindCard.GameLogic.LevelLogic
{
  public class LevelCardDeck
  {
    public readonly CardData WinnerCard;
    public readonly List<CardData> AllCards;
    public readonly int DifficultLevel;

    public LevelCardDeck(CardData winnerCard, List<CardData> allCards, int difficultLevel)
    {
      WinnerCard = winnerCard;
      AllCards = allCards;
      DifficultLevel = difficultLevel;
    }
  }
}