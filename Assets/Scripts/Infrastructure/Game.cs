using FindCard.Infrastructure.Services;
using FindCard.Infrastructure.States;

namespace FindCard.Infrastructure
{
  public class Game
  {
    public readonly GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner)
    {
      AllServices services = new AllServices();
      StateMachine = new GameStateMachine(services);
    }
  }
}