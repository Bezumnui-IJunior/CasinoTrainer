using System;
using UnityEngine;

namespace Windows.Configs
{
    [Serializable]
    public class WindowConfig
    {
        [SerializeField] private WindowsId _id;
        [SerializeField] private Window _prefab;

        public WindowsId Id => _id;
        public Window Prefab => _prefab;
    }
}