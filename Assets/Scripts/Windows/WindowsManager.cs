using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Configs;
using Infrastructure;
using UnityEngine;
using VContainer;

namespace Windows
{
    public class WindowsManager : IWindowsManager
    {
        private readonly IInstantilizer _instantilizer;
        private readonly Dictionary<WindowsId, Window> _openedWindows = new Dictionary<WindowsId, Window>();
        private readonly IWindowsConfig _windowsConfig;

        [Inject]
        public WindowsManager(IWindowsConfig windowsConfig, IInstantilizer instantilizer)
        {
            _windowsConfig = windowsConfig;
            _instantilizer = instantilizer;
        }

        public RectTransform RootUi { get; private set; }

        public void SetRootUI(RectTransform newRoot) =>
            RootUi = newRoot;

        public void Open(WindowsId id)
        {
            if (RootUi == null)
                throw new NullReferenceException("Root UI isn't set or has been deleted");

            if (_openedWindows.ContainsKey(id))
                throw new InvalidOperationException($"Window {id} already opened");

            WindowConfig window = _windowsConfig.Windows.FirstOrDefault(window => window.Id == id);

            if (window == null)
                throw new KeyNotFoundException($"Window {id} was not found");

            _openedWindows.Add(window.Id, _instantilizer.Instantiate(window.Prefab, RootUi));
        }

        public void OpenOrLeaveOnly(params WindowsId[] ids)
        {
            OpenIfClosed(ids);
            CloseExcept(ids);
        }

        public void Close(WindowsId id)
        {
            if (_openedWindows.Remove(id, out Window window))
                window.Destroy();
        }

        public void CloseAll()
        {
            foreach (Window window in _openedWindows.Values)
                window.Destroy();

            _openedWindows.Clear();
        }

        private void OpenIfClosed(WindowsId[] ids)
        {
            foreach (WindowsId windowsId in ids)
            {
                if (_openedWindows.ContainsKey(windowsId) == false)
                    Open(windowsId);
            }
        }

        private void CloseExcept(WindowsId[] ids)
        {
            foreach (WindowsId openedWindow in _openedWindows.Keys)
            {
                if (ids.Contains(openedWindow) == false)
                    Close(openedWindow);
            }
        }
    }
}