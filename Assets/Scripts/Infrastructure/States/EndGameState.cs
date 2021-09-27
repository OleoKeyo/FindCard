using FindCard.GameLogic.UI;
using FindCard.GameLogic.UI.Services;

namespace FindCard.Infrastructure.States
{
  public class EndGameState : IState
  {
    private readonly IGameStateMachine _gameStateMachine;
    private readonly IHudService _hudService;

    public EndGameState(GameStateMachine gameStateMachine, IHudService hudService)
    {
      _gameStateMachine = gameStateMachine;
      _hudService = hudService;
    }
    
    public async void Enter()
    {
      await _hudService.ShowRestartMenu();
      await _hudService.WaitRestartClick();
      await _hudService.HideRestartMenu();
      _gameStateMachine.Enter<RestartGameState>();
    }

    public void Exit()
    {
    }
  }
}