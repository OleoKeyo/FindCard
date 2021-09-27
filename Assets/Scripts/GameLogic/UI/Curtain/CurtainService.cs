using System.Threading.Tasks;
using FindCard.Infrastructure.Factory;

namespace FindCard.GameLogic.UI.Curtain
{
  public class CurtainService : ICurtainService
  {
    private readonly IGameFactory _gameFactory;
    private LoadingCurtain _loadingCurtain;

    public CurtainService(IGameFactory gameFactory)
    {
      _gameFactory = gameFactory;
    }
    
    public Task ShowLoadingCurtain()
    {
      if (_loadingCurtain == null)
      {
        _loadingCurtain = _gameFactory.CreateLoadingCurtain();
      }
      return _loadingCurtain.Show();
    }

    public Task HideLoadingCurtain()
    {
      return _loadingCurtain.Hide();
    }
  }
}