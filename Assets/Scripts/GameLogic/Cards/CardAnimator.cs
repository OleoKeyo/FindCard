using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace FindCard.GameLogic.CellLogic
{
  public class CardAnimator : MonoBehaviour
  {
    [SerializeField] private CardImage animatedImage;
    
    [SerializeField] private float startBounceAnimationTimeInSeconds = 1f;
    [SerializeField] private Vector3 startBounceAnimationPunchScaleStrength = new Vector3(1f, 1f, 1f);
    
    [SerializeField] private float rightAnswerAnimationTimeInSeconds = 1f;
    [SerializeField] private Vector3 rightAnswerImagePunchScaleStrength = new Vector3(0.5f, 0.5f, 0.5f);

    [SerializeField] private float wrongAnswerAnimationTimeInSeconds = 1f;
    [SerializeField] private Vector3 wrongAnswerImagePunchPositionStrength = new Vector3(10f, 0f, 0f);

    private Tween _startBounceTween;
    private Tween _wrongAnswerTween;
    private Tween _rightAnswerTween;

    private Tween _currentPlayingTween;
    
    public void Construct()
    {
      InitStartBounceTween();
      Transform imageTransform = animatedImage.ImageTransform;
      InitWrongAnswerTween(imageTransform);
      InitRightAnswerTween(imageTransform);
      
      _currentPlayingTween = _startBounceTween;
    }

    private void InitRightAnswerTween(Transform imageTransform)
    {
      _rightAnswerTween = imageTransform
        .DOPunchScale(rightAnswerImagePunchScaleStrength, rightAnswerAnimationTimeInSeconds, 0)
        .SetEase(Ease.Linear)
        .SetAutoKill(false)
        .Pause();
    }

    private void InitWrongAnswerTween(Transform imageTransform)
    {
      _wrongAnswerTween = imageTransform
        .DOPunchPosition(wrongAnswerImagePunchPositionStrength, wrongAnswerAnimationTimeInSeconds, 0)
        .SetEase(Ease.InBounce)
        .SetAutoKill(false)
        .Pause();
    }

    private void InitStartBounceTween()
    {
      _startBounceTween = transform
        .DOPunchScale(startBounceAnimationPunchScaleStrength, startBounceAnimationTimeInSeconds, 0)
        .SetEase(Ease.Linear)
        .SetAutoKill(false)
        .Pause();
    }

    public async Task PlayStartBounceAnimation() => 
      await PlayTween(_startBounceTween);

    public async Task PlayWrongAnswerAnimation() => 
      await PlayTween(_wrongAnswerTween);

    public async Task PlayRightAnswerAnimation() => 
      await PlayTween(_rightAnswerTween);

    private async Task PlayTween(Tween tween)
    {
      _currentPlayingTween = tween;
      _currentPlayingTween.Restart();
      await _currentPlayingTween.AsyncWaitForCompletion();
    }
  }
}