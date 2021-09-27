using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace FindCard.GameLogic.UI.Curtain
{
  public class LoadingCurtain : MonoBehaviour
  {
    [SerializeField] private CanvasGroup curtain;
    [SerializeField] private float minCurtainAlpha;
    [SerializeField] private float maxCurtainAlpha;
    [SerializeField] private float fadeAnimationTime;
    
    private Tween _fadeTween;
    private Tween _doFadeIn;
    private Tween _doFadeOut;

    private void Awake()
    {
      InitFadeInTween();
      InitFadeOutTween();
    }

    private void InitFadeOutTween()
    {
      _doFadeOut = curtain
        .DOFade(maxCurtainAlpha, fadeAnimationTime)
        .SetAutoKill(false)
        .Pause();
    }

    private void InitFadeInTween()
    {
      _doFadeIn = curtain
        .DOFade(minCurtainAlpha, fadeAnimationTime)
        .SetAutoKill(false)
        .Pause();
    }

    private void Start()
    {
      curtain.alpha = minCurtainAlpha;
    }

    public Task Show()
    {
      gameObject.SetActive(true);
      return PlayFadeTween(_doFadeOut);
    }
    
    public async Task Hide()
    {
      await PlayFadeTween(_doFadeIn);
      gameObject.SetActive(false);
    }

    private async Task PlayFadeTween(Tween fadeTween)
    {
      _fadeTween = fadeTween;
      _fadeTween.Restart();
      await _fadeTween.AsyncWaitForCompletion();
    }
  }
}