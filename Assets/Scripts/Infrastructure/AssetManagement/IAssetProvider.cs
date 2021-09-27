using FindCard.Infrastructure.Services;
using UnityEngine;

namespace FindCard.Infrastructure.AssetManagement
{
  public interface IAssetProvider : IService
  {
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Transform parent);
  }
}