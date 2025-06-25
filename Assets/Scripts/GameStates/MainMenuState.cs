using Windows;
using Infrastructure;
using Notifications;
using Unity.IL2CPP.CompilerServices;
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
        private readonly INotificationService _notificationService;

        [Inject]
        public MainMenuState(
            IStateMachine stateMachine,
            ISceneFactory sceneFactory,
            IWindowsManager windowsManager,
            INotificationService notificationService) : base(stateMachine)
        {
            _sceneFactory = sceneFactory;
            _windowsManager = windowsManager;
            _notificationService = notificationService;
        }

        public override void Enter()
        {
            _windowsManager.CloseAll();
            _sceneFactory.LoadScene(MainMenuScenePath, ChangeState<ActualizeState>);

            if (_notificationService.IsNotificationAllowed() == false)
                _notificationService.RequestAuthorization();
        }
    }
}