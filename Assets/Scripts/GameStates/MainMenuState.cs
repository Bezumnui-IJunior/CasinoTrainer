using Windows;
using Infrastructure;
using Unity.IL2CPP.CompilerServices;
using UnityEditor;
using VContainer;

namespace GameStates
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class MainMenuState : State
    {
        private const string MainMenuScenePath = "Assets/Resources/Scenes/MainMenu.unity";
        
        private readonly ISceneFactory _sceneFactory;
        private readonly IWindowsManager _windowsManager;

        [Inject]
        public MainMenuState(IStateMachine stateMachine, ISceneFactory sceneFactory, IWindowsManager windowsManager) : base(stateMachine)
        {
            _sceneFactory = sceneFactory;
            _windowsManager = windowsManager;
        }

        public override void Enter()
        {
            _windowsManager.CloseAll();
            _sceneFactory.LoadScene(MainMenuScenePath);
        }
        
    }
}