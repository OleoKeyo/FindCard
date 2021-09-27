using System.Collections.Generic;
using FindCard.GameLogic.LevelLogic;
using FindCard.Infrastructure.Random;
using FindCard.StaticData.Cards;
using FindCard.StaticData.Config;

namespace FindCard.GameLogic.CellLogic
{
  public class CardDealer
  {
    private readonly IConfigService _configService;
    private readonly IRandomService _random;

    private readonly CardSequence _cardSequence;
    private readonly CardSequence _winnerSequence;
    
    public CardDealer(IConfigService configService, IRandomService random, CardSetLibrary set)
    {
      _configService = configService;
      _random = random;
      IReadOnlyList<CardData> cardSet = set.CardSet;
      _cardSequence = new CardSequence(_random, cardSet);
      _winnerSequence = new CardSequence(_random, cardSet);
    }

    public LevelCardDeck CreateCardSet(int difficultLevel)
    {
      List<CardData> cards = new List<CardData>();
      CardData winnerCard = _winnerSequence.GetCard();
      _cardSequence.RemoveCard(winnerCard);

      for (int i = 0; i < difficultLevel * _configService.CellsInLine - 1; i++)
      {
        CardData cardData = _cardSequence.GetCard();
        cards.Add(cardData);
      }

      int randomPositionInList = _random.Next(0, cards.Count);
      cards.Insert(randomPositionInList, winnerCard);
      _cardSequence.Reset();

      LevelCardDeck levelCardDeck = new LevelCardDeck(winnerCard, cards, difficultLevel);
      return levelCardDeck;
    }

    public void Reset()
    {
      _winnerSequence.Reset();
    }
  }
}