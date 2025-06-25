using System;
using Windows;
using Infrastructure;
using Progress;
using Unity.IL2CPP.CompilerServices;
using VContainer;

namespace GameStates
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class ActualizeState : State
    {
        private readonly IPlayerData _playerData;
        private readonly IWindowsManager _windowsManager;

        [Inject]
        public ActualizeState(
            IStateMachine stateMachine,
            IWindowsManager windowsManager,
            IPlayerData playerData) : base(stateMachine)
        {
            _windowsManager = windowsManager;
            _playerData = playerData;
        }

        public override void Enter()
        {
            // if (_playerData.LastBonusReceived == DateTime.MinValue)
            // {
            //     _playerData.LastBonusReceived = DateTime.Now;
            //     _playerData.Save();
            // }

            if (_playerData.WhenCanReceiveBonus <= DateTime.Now)
                _windowsManager.Open(WindowsId.MoneyBonusWindow);
        }
    }
}