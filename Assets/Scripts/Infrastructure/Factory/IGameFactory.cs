using FindCard.GameLogic.BoardLogic;
using FindCard.GameLogic.CellLogic;
using FindCard.GameLogic.UI;
using FindCard.GameLogic.UI.Curtain;
using FindCard.GameLogic.UI.Services;
using FindCard.Infrastructure.Services;
using FindCard.StaticData.Cards;
using UnityEngine;

namespace FindCard.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    Card CreateCard(GameBoard gameBoard, CardData cardData, Transform parent);
    GameBoard CreateGameBoard();
    LoadingCurtain CreateLoadingCurtain();
    RestartHud CreateRestartHud();
  }
}