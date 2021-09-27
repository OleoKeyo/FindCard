using FindCard.GameLogic.BoardLogic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FindCard.GameLogic.CellLogic
{
  public class CardAnswerChecker : MonoBehaviour, IPointerClickHandler
  {
    private IGameBoardService _gameBoardService;
    private string _answer;
    private bool _isClicked;
    private Card _card;

    public void Construct(IGameBoardService gameBoardService, Card card, string answer)
    {
      _gameBoardService = gameBoardService;
      _card = card;
      _answer = answer;
    }
    
    public async void OnPointerClick(PointerEventData eventData)
    {
      if(_isClicked)
        return;
      
      _isClicked = true;
      await _gameBoardService.OnCardClick(_card, _answer);
      _isClicked = false;
    }

    public void SetAnswer(string answer)
    {
      _answer = answer;
    }
  }
}