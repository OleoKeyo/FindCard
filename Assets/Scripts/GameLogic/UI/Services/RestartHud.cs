using System.Threading.Tasks;
using FindCard.GameLogic.UI.Curtain;
using UnityEngine;
using UnityEngine.UI;

namespace FindCard.GameLogic.UI.Services
{
  public class RestartHud : MonoBehaviour
  {
    [SerializeField] private LoadingCurtain curtain;
    [SerializeField] private Button restartButton;
    
    public Task Show()
    {
      restartButton.gameObject.SetActive(true);
      return curtain.Show();
    }

    public Task WaitRestartClick()
    {
      restartButton.onClick.AddListener(CloseRestartButton);
      var promise = new TaskCompletionSource<bool>();
      restartButton.onClick.AddListener(() => promise.SetResult(true));
      return promise.Task;
    }

    public Task Hide()
    {
      restartButton.onClick.RemoveAllListeners();
      return curtain.Hide();
    }

    private void CloseRestartButton()
    {
      restartButton.gameObject.SetActive(false);
    }
  }
}