using System;
using System.Threading.Tasks;
using FindCard.GameLogic.CellLogic;
using FindCard.GameLogic.LevelLogic;
using FindCard.Infrastructure.Services;

namespace FindCard.GameLogic.BoardLogic
{
  public interface IGameBoardService : IService
  {
    Task Refresh(LevelCardDeck currentLevelCardDeck);
    int DifficultLevel { get; set; }
    Action OnChoiceRightAnswer { get; set; }
    Task OnCardClick(Card card, string answer);
    void ResetGameBoard();
  }
}