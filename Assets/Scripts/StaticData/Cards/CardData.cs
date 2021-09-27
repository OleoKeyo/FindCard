using System;
using UnityEngine;

namespace FindCard.StaticData.Cards
{
  [Serializable]
  public class CardData
  {
    public string description;
    public Sprite sprite;
    public Color backgroundColor;
    public bool isNeededToRotate;
  }
}
