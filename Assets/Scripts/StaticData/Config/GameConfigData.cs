using System.Collections.Generic;
using FindCard.StaticData.Cards;
using UnityEngine;

namespace FindCard.StaticData.Config
{
  [CreateAssetMenu(fileName = "GameConfigData", menuName = "GameConfig")]
  public class GameConfigData : ScriptableObject
  {
    public int cellsInLine;
    public Vector2Int cellSizeInPixels;
    public List<CardSetData> cardSets;
    public int lastLevel;
  }
}