using FindCard.GameLogic.BoardLogic;
using FindCard.GameLogic.CellLogic;
using FindCard.GameLogic.UI;
using FindCard.GameLogic.UI.Curtain;
using FindCard.GameLogic.UI.Services;
using FindCard.Infrastructure.AssetManagement;
using FindCard.Infrastructure.Services;
using FindCard.Infrastructure.States;
using FindCard.StaticData.Cards;
using FindCard.StaticData.Config;
using UnityEngine;

namespace FindCard.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly AllServices _services;
    private readonly IAssetProvider _assetProvider;
    
    private Transform _uiRoot;

    public GameFactory(AllServices allServices)
    {
      _services = allServices;
      _assetProvider = _services.Resolve<IAssetProvider>();
    }

    public Card CreateCard(GameBoard gameBoard, CardData cardData, Transform parent) 
    {
      GameObject prefab = Instantiate(AssetPath.CellPath, parent);
      Card card = prefab.GetComponent<Card>();
      card.Construct(_services.Resolve<IGameBoardService>(), cardData);
      return card;
    }

    public GameBoard CreateGameBoard()
    {
      if (_uiRoot == null)
      {
        CreateUIRoot();
      }
      
      GameObject prefab = Instantiate(AssetPath.GameBoardPath, _uiRoot);
      GameBoard gameBoard = prefab.GetComponent<GameBoard>();
      gameBoard.Construct(this, _services.Resolve<IConfigService>());
      return gameBoard;
    }

    public LoadingCurtain CreateLoadingCurtain()
    {
      GameObject prefab = Instantiate(AssetPath.CurtainPath);
      LoadingCurtain curtain = prefab.GetComponent<LoadingCurtain>();
      return curtain;
    }

    private void CreateUIRoot()
    {
      GameObject root = Instantiate(AssetPath.UIRootPath);
      _uiRoot = root.transform;
    }

    public RestartHud CreateRestartHud()
    {
      GameObject prefab = Instantiate(AssetPath.RestartHudPath);
      RestartHud restartHud = prefab.GetComponent<RestartHud>();
      return restartHud;
    }

    private GameObject Instantiate(string prefabPath) =>
      _assetProvider.Instantiate(prefabPath);

    private GameObject Instantiate(string prefabPath, Transform parent) => 
      _assetProvider.Instantiate(prefabPath, parent);
  }
}