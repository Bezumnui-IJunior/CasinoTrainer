using Infrastructure;
using Unity.IL2CPP.CompilerServices;
using VContainer;

namespace GameStates
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class LoaderState : State
    {
        private const string ScenePath = "Assets/Resources/Scenes/Loader.unity";
        private readonly ISceneFactory _sceneFactory;

        [Inject]
        public LoaderState(IStateMachine stateMachine, ISceneFactory sceneFactory) : base(stateMachine)
        {
            _sceneFactory = sceneFactory;
        }

        public override void Enter()
        {
            _sceneFactory.LoadScene(ScenePath, ChangeState<SetupMusicState>);
        }
    }
}