using FindCard.Infrastructure.Services;

namespace FindCard.GameLogic.LevelLogic.Services
{
  public interface ILevelGeneratorService : IService
  {
    LevelCardDeck GenerateLevelConfig(int difficultLevel);
    void Reset();
  }
}