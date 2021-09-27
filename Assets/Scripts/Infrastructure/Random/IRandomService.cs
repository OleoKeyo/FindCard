using FindCard.Infrastructure.Services;

namespace FindCard.Infrastructure.Random
{
  public interface IRandomService : IService
  {
    int Next(int maxValue);
    int Next(int minValue, int maxValue);
  }
}