using System.Collections.Generic;
using UnityEngine;

namespace Windows.Configs
{
    [CreateAssetMenu(fileName = "WindowsConfig", menuName = "ECS Configs / WindowsConfig", order = 51)]
    public class WindowsConfig : ScriptableObject, IWindowsConfig
    {
        [SerializeField] private List<WindowConfig> _windows;

        public IReadOnlyList<WindowConfig> Windows => _windows;
    }
}