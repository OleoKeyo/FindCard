using System.Collections.Generic;
using FindCard.GameLogic.CellLogic;
using UnityEngine;

namespace FindCard.StaticData.Cards
{
  [CreateAssetMenu(fileName = "CardSetData", menuName = "CardSet")]
  public class CardSetData : ScriptableObject
  {
    public List<CardData> Symbols;
  }
}