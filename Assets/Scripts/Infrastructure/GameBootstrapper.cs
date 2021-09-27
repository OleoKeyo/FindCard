using FindCard.Infrastructure.States;
using UnityEngine;

namespace FindCard.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    private Game _game;

    private void Awake()
    {
      _game = new Game(this);
      _game.StateMachine.Enter<BootstrapState>();
    }
  }
}