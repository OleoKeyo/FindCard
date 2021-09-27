using FindCard.GameLogic.BoardLogic;
using FindCard.StaticData.Cards;
using UnityEngine;

namespace FindCard.GameLogic.CellLogic
{
  public class Card : MonoBehaviour
  {
    public CardAnimator animator;
    public CardImage image;
    public CardAnswerChecker cardAnswerChecker;
    
    public void Construct(IGameBoardService gameBoardService,CardData data)
    {
      animator.Construct();
      cardAnswerChecker.Construct(gameBoardService, this, data.description);
      image.UpdateImage(data);
    }
  }
}