using System.Threading.Tasks;
using FindCard.Infrastructure.Services;

namespace FindCard.GameLogic.UI.Curtain
{
  public interface ICurtainService : IService
  {
    public Task ShowLoadingCurtain();
    public Task HideLoadingCurtain();
  }
}