using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace FindCard.GameLogic.UI
{
  public class FadeText : MonoBehaviour
  {
    [SerializeField] private TMP_Text text;
    [SerializeField] private float maxTextAlpha;
    [SerializeField] private float fadeAnimationTime;
    
    private Tween _doFadeOut;

    public void Construct()
    {
      InitFadeTween();
    }

    private void InitFadeTween()
    {
      _doFadeOut = text
        .DOFade(maxTextAlpha, fadeAnimationTime)
        .SetAutoKill(false)
        .Pause();
    }

    public void ShowFadedText(string text)
    {
      this.text.text = text;
      this.text.alpha = 0f;
      gameObject.SetActive(true);
      PlayFadeTween();
    }
    
    private void PlayFadeTween()
    {
      _doFadeOut.Restart();
    }
  }
}