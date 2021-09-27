using System.Threading.Tasks;
using FindCard.Infrastructure.Factory;

namespace FindCard.GameLogic.UI.Services
{
  public class HudService : IHudService
  {
    private readonly IGameFactory _gameFactory;
    private RestartHud _restartHud;

    public HudService(IGameFactory gameFactory)
    {
      _gameFactory = gameFactory;
    }
    
    public Task ShowRestartMenu()
    {
      if (_restartHud == null)
      {
        _restartHud = _gameFactory.CreateRestartHud();
      }

      return _restartHud.Show();
    }

    public Task WaitRestartClick()
    {
      return _restartHud.WaitRestartClick();
    }

    public Task HideRestartMenu()
    {
      return _restartHud.Hide();
    }
  }
}