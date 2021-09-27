using System;
using System.Collections.Generic;
using FindCard.GameLogic.BoardLogic;
using FindCard.GameLogic.LevelLogic.Services;
using FindCard.GameLogic.UI;
using FindCard.GameLogic.UI.Curtain;
using FindCard.GameLogic.UI.Services;
using FindCard.Infrastructure.Factory;
using FindCard.Infrastructure.Services;
using FindCard.StaticData.Config;

namespace FindCard.Infrastructure.States
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(AllServices services)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, services),
        [typeof(LoadLevelState)] = new LoadLevelState(
          this,
          services.Resolve<ILevelGeneratorService>(),
          services.Resolve<IConfigService>(),
          services.Resolve<IGameBoardService>()),
        [typeof(EndGameState)] = new EndGameState(this, services.Resolve<IHudService>()),
        [typeof(RestartGameState)] = new RestartGameState(
          this, 
          services.Resolve<ILevelGeneratorService>(), 
          services.Resolve<ICurtainService>(), 
          services.Resolve<IGameBoardService>())
      };
    }

    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      IPayloadedState<TPayload> state = ChangeState<TState>();
      state.Enter(payload);
    }    
    
    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();
      
      TState state = GetState<TState>();
      _activeState = state;
      
      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;
  }
}