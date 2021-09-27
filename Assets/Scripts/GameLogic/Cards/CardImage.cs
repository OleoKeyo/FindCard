using FindCard.StaticData.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace FindCard.GameLogic.CellLogic
{
  public class CardImage : MonoBehaviour
  {
    [SerializeField] private Image symbolImage;
    [SerializeField] private Image backgroundImage;

    private readonly Vector3 _rotationAngle = new Vector3(0, 0, -90);
    public Transform ImageTransform => symbolImage.transform;

    public void UpdateImage(CardData cardData)
    {
      symbolImage.transform.rotation = cardData.isNeededToRotate ? Quaternion.Euler(_rotationAngle): Quaternion.Euler(Vector3.zero);
      symbolImage.sprite = cardData.sprite;
      backgroundImage.color = cardData.backgroundColor;
    }
  }
}