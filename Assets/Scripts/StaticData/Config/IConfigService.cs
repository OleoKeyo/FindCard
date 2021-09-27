using System.Collections.Generic;
using FindCard.Infrastructure.Services;
using FindCard.StaticData.Cards;
using UnityEngine;

namespace FindCard.StaticData.Config
{
  public interface IConfigService : IService
  {
    IReadOnlyDictionary<string, CardSetLibrary> CardSetLibraries { get; }
    int CellsInLine { get; }
    int LastLevel { get; }
    Vector2Int CellSizeInPixels { get; }
  }
}