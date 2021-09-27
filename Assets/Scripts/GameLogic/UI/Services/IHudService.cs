using System.Threading.Tasks;
using FindCard.Infrastructure.Services;

namespace FindCard.GameLogic.UI.Services
{
  public interface IHudService : IService
  {
    Task ShowRestartMenu();
    Task WaitRestartClick();
    Task HideRestartMenu();
  }
}