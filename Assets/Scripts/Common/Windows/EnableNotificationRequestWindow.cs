using System;
using Windows;
using Notifications;
using Sounds.Configs;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using View;

namespace Common.Windows
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class EnableNotificationRequestWindow : Window
    {
        [SerializeField] private Button _allowButton;

        private INotificationService _notificationService;
        private IWindowsManager _windowsManager;
        private INotificationsFactory _notificationsFactory;
        private IMusicConfig _musicConfig;
        private ISoundFXService _soundFXService;

        protected override void Initialize()
        {
            _soundFXService.PlayClip(_musicConfig.InfoSound);
            _allowButton.onClick.AddListener(OnClick);
        }

        protected override void Deinitialize() =>
            _allowButton.onClick.RemoveListener(OnClick);

        private void OnClick()
        {
            _windowsManager.Close(WindowsId.EnableNotificationRequestWindow);
            _notificationService.OpenNotificationSettings();
            _notificationsFactory.ScheduleRewardReminder();
        }

        [Inject]
        private void Construct(INotificationService notificationService, IWindowsManager windowsManager, INotificationsFactory notificationsFactory, IMusicConfig musicConfig, ISoundFXService soundFXService)
        {
            _notificationService = notificationService;
            _windowsManager = windowsManager;
            _notificationsFactory = notificationsFactory;
            _musicConfig = musicConfig;
            _soundFXService = soundFXService;
        }
    }
}