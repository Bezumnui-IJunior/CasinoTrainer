using Infrastructure;
using Unity.IL2CPP.CompilerServices;
using VContainer;

namespace GameStates
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class BlackJackStartState : State
    {
        private const string BlackjackScene = "Assets/Resources/Scenes/BlackJack.unity";
        private readonly ISceneFactory _sceneFactory;

        [Inject]
        public BlackJackStartState(IStateMachine stateMachine, ISceneFactory sceneFactory) : base(stateMachine)
        {
            _sceneFactory = sceneFactory;
        }

        public override void Enter()
        {
            _sceneFactory.LoadScene(BlackjackScene, OnLoaded);
        }

        private void OnLoaded()
        {
            ChangeState<BlackJackRunningState>();
        }

    }
}