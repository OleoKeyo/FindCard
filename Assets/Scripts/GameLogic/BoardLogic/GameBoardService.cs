using System;
using System.Threading.Tasks;
using FindCard.GameLogic.CellLogic;
using FindCard.GameLogic.LevelLogic;
using FindCard.Infrastructure.Factory;

namespace FindCard.GameLogic.BoardLogic
{
  public class GameBoardService : IGameBoardService
  {
    public int DifficultLevel { get; set; }
    public Action OnChoiceRightAnswer { get; set; }

    private readonly IGameFactory _gameFactory;
    private GameBoard _gameBoard;
    private bool _isReadyForCheck;
    private string _rightAnswer;

    public GameBoardService(IGameFactory gameFactory)
    {
      _gameFactory = gameFactory;
    }
    
    public async Task Refresh(LevelCardDeck levelCardDeck)
    {
      if (_gameBoard == null)
      {
        _gameBoard = _gameFactory.CreateGameBoard();
      }

      DifficultLevel = levelCardDeck.DifficultLevel;
      await _gameBoard.Refresh(levelCardDeck);
      SetRightAnswer(levelCardDeck.WinnerCard.description);
    }

    public async Task OnCardClick(Card clickedCard, string answer)
    {
      if (!_isReadyForCheck)
        return;
      if (AnswerIsRight(answer))
      {
        await clickedCard.animator.PlayRightAnswerAnimation();
        OnChoiceRightAnswer?.Invoke();
      }
      else
      {
        await clickedCard.animator.PlayWrongAnswerAnimation();
      }
    }

    public void ResetGameBoard()
    {
      DifficultLevel = 0;
      _gameBoard.Clear();
      ResetRightAnswer();
    }

    private void ResetRightAnswer()
    {
      _rightAnswer = null;
      _isReadyForCheck = false;
    }

    private bool AnswerIsRight(string answer) =>
      answer == _rightAnswer;

    private void SetRightAnswer(string rightAnswer)
    {
      _rightAnswer = rightAnswer;
      _isReadyForCheck = true;
    }
  }
}