using System.Collections.Generic;
using FindCard.StaticData.Cards;
using UnityEngine;

namespace FindCard.StaticData.Config
{
  public class ConfigService : IConfigService
  {
    private const string StaticDataPath = "StaticData/GameConfigData";

    private Dictionary<string, CardSetLibrary> _cardSetLibraries;

    public IReadOnlyDictionary<string, CardSetLibrary> CardSetLibraries => _cardSetLibraries;

    public int CellsInLine { get; }
    public int LastLevel { get; }

    public Vector2Int CellSizeInPixels { get; }

    public ConfigService()
    {
      GameConfigData gameConfig = GetGameConfig();
      CellsInLine = gameConfig.cellsInLine;
      CellSizeInPixels = gameConfig.cellSizeInPixels;
      LastLevel = gameConfig.lastLevel;
      
      InitCardSetLibrary(gameConfig);
    }

    private void InitCardSetLibrary(GameConfigData gameConfig)
    {
      _cardSetLibraries = new Dictionary<string, CardSetLibrary>();
      foreach (CardSetData cardSet in gameConfig.cardSets)
      {
        string symbolSetName = cardSet.name;
        _cardSetLibraries[symbolSetName] = new CardSetLibrary(cardSet.Symbols);
      }
    }

    private GameConfigData GetGameConfig() => 
      Resources.Load<GameConfigData>(StaticDataPath);
  }
}