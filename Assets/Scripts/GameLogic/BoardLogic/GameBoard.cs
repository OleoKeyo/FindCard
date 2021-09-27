using System.Collections.Generic;
using System.Threading.Tasks;
using FindCard.GameLogic.CellLogic;
using FindCard.GameLogic.LevelLogic;
using FindCard.GameLogic.UI;
using FindCard.Infrastructure.Factory;
using FindCard.StaticData.Cards;
using FindCard.StaticData.Config;
using UnityEngine;
using UnityEngine.UI;

namespace FindCard.GameLogic.BoardLogic
{
  public class GameBoard : MonoBehaviour
  {
    [SerializeField] private int paddingInPixels;
    [SerializeField] private Vector2Int spacing;
    [SerializeField] private RectTransform content;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private FadeText currentTargetText;

    public int currentDifficultLevel;

    private IGameFactory _gameFactory;
    private Vector2Int _cellSizeInPixels;
    private int _cellsInLine;
    private readonly List<Card> _activeCards = new List<Card>();
    
    public void Construct(IGameFactory gameFactory, IConfigService configService)
    {
      _gameFactory = gameFactory;
      _cellsInLine = configService.CellsInLine;
      _cellSizeInPixels = configService.CellSizeInPixels;
      currentTargetText.Construct();
      SetPadding();
      SetSpacing();
      SetGridSize();
    }

    public async Task Refresh(LevelCardDeck levelCardDeck)
    {
      currentDifficultLevel = levelCardDeck.DifficultLevel;
      content.sizeDelta = CalculateRectSize();
      SetCurrentTargetText(levelCardDeck);
      await RefreshCards(levelCardDeck);
    }
    
    public void Clear()
    {
      foreach (Card activeCard in _activeCards)
        Destroy(activeCard.gameObject);
      
      _activeCards.Clear();
    }
    
    private async Task RefreshCards(LevelCardDeck levelCardDeck)
    {
      List<CardData> cards = levelCardDeck.AllCards;
      for (int index = 0; index < cards.Count; index++)
      {
        CardData cardData = cards[index];
        if (index < _activeCards.Count)
          UpdateCard(_activeCards[index], cardData);
        else
          await CreateNewCard(cardData);
      }
    }

    private async Task CreateNewCard(CardData cardData)
    {
      Card card = _gameFactory.CreateCard(this, cardData, content);
      _activeCards.Add(card);
      if (currentDifficultLevel == 1)
        await card.animator.PlayStartBounceAnimation();
    }

    private void UpdateCard(Card card, CardData data)
    {
      card.cardAnswerChecker.SetAnswer(data.description);
      card.image.UpdateImage(data);
    }

    private void SetCurrentTargetText(LevelCardDeck levelCardDeck)
    {
      string text = $"Find {levelCardDeck.WinnerCard.description}";
      currentTargetText.ShowFadedText(text);
    }
    
    private Vector2 CalculateRectSize() =>
      new Vector2
      {
        x = gridLayoutGroup.padding.left + _cellsInLine  * (_cellSizeInPixels.x + spacing.x) - spacing.x + gridLayoutGroup.padding.right,
        y = gridLayoutGroup.padding.top + currentDifficultLevel * (_cellSizeInPixels.y + spacing.y) - spacing.y + gridLayoutGroup.padding.bottom
      };

    private void SetPadding()
    {
      gridLayoutGroup.padding.left = paddingInPixels;
      gridLayoutGroup.padding.right = paddingInPixels;
      gridLayoutGroup.padding.bottom = paddingInPixels;
      gridLayoutGroup.padding.top = paddingInPixels;
    }

    private void SetSpacing()
    {
      gridLayoutGroup.spacing = spacing;
    }

    private void SetGridSize()
    {
      gridLayoutGroup.cellSize = _cellSizeInPixels;
    }
  }
}