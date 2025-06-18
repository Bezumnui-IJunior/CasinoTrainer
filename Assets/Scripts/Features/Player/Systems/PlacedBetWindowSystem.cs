using Windows;
using Features.View.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Player.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class PlacedBetWindowSystem : ICleanupSystem
    {
        private readonly IWindowsManager _windowsManager;
        private Filter _requestFilter;

        public PlacedBetWindowSystem(IWindowsManager windowsManager)
        {
            _windowsManager = windowsManager;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _requestFilter = World.Filter
                .With<PlaceBetRequest>()
                .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity _ in _requestFilter)
                _windowsManager.OpenOrLeaveOnly(WindowsId.BlackJackPlayButtons, WindowsId.MoneyWindow, WindowsId.ScoreCounter);
        }

        public void Dispose() { }
    }
}