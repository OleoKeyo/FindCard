using FindCard.GameLogic.BoardLogic;
using FindCard.GameLogic.LevelLogic;
using FindCard.GameLogic.LevelLogic.Services;
using FindCard.StaticData.Config;
using UnityEngine;

namespace FindCard.Infrastructure.States
{
  public class LoadLevelState : IState
  {
    private readonly GameStateMachine _gameStateMachine;
    private readonly ILevelGeneratorService _levelGeneratorService;
    private readonly IConfigService _configService;
    private readonly IGameBoardService _gameBoardService;

    private int _difficultLevel;
    private LevelCardDeck _cardDeck;
    
    public LoadLevelState(
      GameStateMachine gameStateMachine,
      ILevelGeneratorService levelGeneratorService,
      IConfigService configService,
      IGameBoardService gameBoardService)
    {
      _gameStateMachine = gameStateMachine;
      _levelGeneratorService = levelGeneratorService;
      _configService = configService;
      _gameBoardService = gameBoardService;
    }

    public async void Enter()
    {
      _difficultLevel = _gameBoardService.DifficultLevel + 1;
      
      _cardDeck = _levelGeneratorService.GenerateLevelConfig(_difficultLevel);
      await _gameBoardService.Refresh(_cardDeck);
      _gameBoardService.OnChoiceRightAnswer += LoadNextLevel;
      
      Debug.Log($"GameInit. DifficultLevel {_cardDeck.DifficultLevel}" );
    }
    
    public void Exit()
    {
      _gameBoardService.OnChoiceRightAnswer -= LoadNextLevel;
      Debug.Log($"Exit level {_difficultLevel}");
    }
    
    private void LoadNextLevel()
    {
      int difficultLevel = _gameBoardService.DifficultLevel;
      
      if (difficultLevel == _configService.LastLevel)
      {
        _gameStateMachine.Enter<EndGameState>();
      }
      else
      {
        _gameStateMachine.Enter<LoadLevelState>();
      }
    }
  }
}