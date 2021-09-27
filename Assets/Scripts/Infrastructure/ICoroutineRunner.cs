using System.Collections;
using UnityEngine;

namespace FindCard.Infrastructure
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}