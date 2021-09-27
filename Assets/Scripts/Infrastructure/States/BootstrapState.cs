using FindCard.GameLogic.BoardLogic;
using FindCard.GameLogic.LevelLogic.Services;
using FindCard.GameLogic.UI;
using FindCard.GameLogic.UI.Curtain;
using FindCard.GameLogic.UI.Services;
using FindCard.Infrastructure.AssetManagement;
using FindCard.Infrastructure.Factory;
using FindCard.Infrastructure.Random;
using FindCard.Infrastructure.Services;
using FindCard.StaticData.Config;
using UnityEngine;

namespace FindCard.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly AllServices _services;
    
    public BootstrapState(GameStateMachine stateMachine, AllServices services)
    {
      _stateMachine = stateMachine;
      _services = services;

      RegisterServices();
    }

    private void RegisterServices()
    {
      _services.RegisterSingle<IGameStateMachine>(_stateMachine);
      _services.RegisterSingle<IAssetProvider>(new AssetProvider());
      _services.RegisterSingle<IRandomService>(new RandomService());
      _services.RegisterSingle<IConfigService>(new ConfigService());
      _services.RegisterSingle<ILevelGeneratorService>(new LevelGeneratorService(
        _services.Resolve<IConfigService>(), 
        _services.Resolve<IRandomService>()));

      _services.RegisterSingle<IGameFactory>(new GameFactory(_services));
      _services.RegisterSingle<IGameBoardService>(new GameBoardService(_services.Resolve<IGameFactory>()));
      _services.RegisterSingle<IHudService>(new HudService(_services.Resolve<IGameFactory>()));
      _services.RegisterSingle<ICurtainService>(new CurtainService(_services.Resolve<IGameFactory>()));
    }

    public void Enter()
    {
      _stateMachine.Enter<LoadLevelState>();
    }

    public void Exit()
    {
      Debug.Log("ExitBootstrapState");
    }
  }
}