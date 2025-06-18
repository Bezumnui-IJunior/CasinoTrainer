using Windows;
using Features.BlackJack.Components;
using Features.GameOver.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using VContainer;

namespace Features.GameOver.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class GameOverDrawSystem : ISystem
    {
        private readonly IWindowsManager _windowsManager;
        private Stash<DrownTag> _drownTag;
        private Filter _filter;
        private Stash<WinnerComponent> _winner;

        [Inject]
        public GameOverDrawSystem(IWindowsManager windowsManager)
        {
            _windowsManager = windowsManager;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<GameOverTag>()
                .Without<DrownTag>()
                .Build();

            _drownTag = World.GetStash<DrownTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                _drownTag.Add(entity);
                _windowsManager.OpenOrLeaveOnly(WindowsId.DrawWindow, WindowsId.MoneyWindow);
            }
        }

        public void Dispose() { }
    }
}