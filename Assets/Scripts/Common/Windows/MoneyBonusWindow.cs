using System;
using Windows;
using Notifications;
using Progress;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Common.Windows
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class MoneyBonusWindow : Window
    {
        [SerializeField] private Button _collectButton;

        private IPlayerData _playerData;
        private IWindowsManager _windowsManager;
        private INotificationService _notificationService;
        private ISettings _settings;
        private INotificationsFactory _notificationsFactory;

        protected override void Initialize() =>
            _collectButton.onClick.AddListener(OnClick);

        protected override void Deinitialize() =>
            _collectButton.onClick.RemoveListener(OnClick);

        private void OnClick()
        {
            _playerData.LastBonusReceived = DateTime.Now;
            _playerData.PlayerMoney += SettingsConstants.AddBonusValue;
            _playerData.Save();

            _windowsManager.Close(WindowsId.MoneyBonusWindow);

            if (_notificationService.IsNotificationAllowed() == false)
                _windowsManager.Open(WindowsId.EnableNotificationRequestWindow);
            else
                _notificationsFactory.ScheduleRewardReminder();
        }

        [Inject]
        private void Construct(IPlayerData playerData, IWindowsManager windowsManager, INotificationsFactory notificationsFactory, ISettings settings, INotificationService notificationService)
        {
            _playerData = playerData;
            _windowsManager = windowsManager;
            _notificationsFactory = notificationsFactory;
            _settings = settings;
            _notificationService = notificationService;
        }
    }
}