using System;
using Windows;
using Infrastructure;
using UnityEngine;
using VContainer;

namespace View
{
    public class UIInitializer : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        private IWindowsManager _windowsManager;

        private void Awake()
        {
            this.DoSelfInjection();

            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            _windowsManager.SetRootUI(_rectTransform);
        }

        private void OnDisable()
        {
            if (_windowsManager.RootUi == _rectTransform)
                _windowsManager.SetRootUI(null);
        }

        [Inject]
        private void Constructor(IWindowsManager windowsManager)
        {
            _windowsManager = windowsManager;
        }
    }
}