using FindCard.GameLogic.BoardLogic;
using FindCard.GameLogic.LevelLogic.Services;
using FindCard.GameLogic.UI.Curtain;

namespace FindCard.Infrastructure.States
{
  public class RestartGameState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly ILevelGeneratorService _levelGeneratorService;
    private readonly ICurtainService _curtain;
    private readonly IGameBoardService _gameBoardService;
    
    public RestartGameState(
      GameStateMachine stateMachine, 
      ILevelGeneratorService levelGeneratorService,
      ICurtainService curtain,
      IGameBoardService gameBoardService)
    {
      _stateMachine = stateMachine;
      _levelGeneratorService = levelGeneratorService;
      _curtain = curtain;
      _gameBoardService = gameBoardService;
    }

    public async void Enter()
    {
      await _curtain.ShowLoadingCurtain();

      _gameBoardService.ResetGameBoard();
      _levelGeneratorService.Reset();
      _stateMachine.Enter<LoadLevelState>();
    }

    public void Exit()
    {
      _curtain.HideLoadingCurtain();
    }
  }
}