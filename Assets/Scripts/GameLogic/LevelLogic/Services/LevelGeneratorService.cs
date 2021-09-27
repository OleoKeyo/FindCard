using System.Collections.Generic;
using FindCard.GameLogic.CellLogic;
using FindCard.Infrastructure.Random;
using FindCard.StaticData.Cards;
using FindCard.StaticData.Config;

namespace FindCard.GameLogic.LevelLogic.Services
{
  public class LevelGeneratorService : ILevelGeneratorService
  {
    private readonly IConfigService _configService;
    private readonly IRandomService _randomService;
    
    private List<CardDealer> _cardDealers;
    
    public LevelGeneratorService(IConfigService configService, IRandomService randomService)
    {
      _configService = configService;
      _randomService = randomService;

      CreateCardDealers();
    }

    private void CreateCardDealers()
    {
      IReadOnlyDictionary<string, CardSetLibrary> cardSets = _configService.CardSetLibraries;
      _cardDealers = new List<CardDealer>(cardSets.Count);
      
      foreach (KeyValuePair<string, CardSetLibrary> cardPair in cardSets)
        _cardDealers.Add(new CardDealer(_configService,_randomService, cardPair.Value));
    }

    public LevelCardDeck GenerateLevelConfig(int difficultLevel)
    {
      int i = _randomService.Next(_cardDealers.Count);
      LevelCardDeck levelCardDeck = _cardDealers[i].CreateCardSet(difficultLevel);
      return levelCardDeck;
    }

    public void Reset()
    {
      foreach (CardDealer cardDealer in _cardDealers)
        cardDealer.Reset();
    }
  }
}