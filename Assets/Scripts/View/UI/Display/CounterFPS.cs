using System.Collections.Generic;
using TMPro;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace View.UI.Display
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class CounterFPS : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        private readonly List<float> _delays = new List<float>();
        private const int Delay = 180;

        private void Update()
        {
            _delays.Add(Time.deltaTime);

            if (_delays.Count < Delay)
                return;

            _textMeshPro.text = $"{(int) GetFramerate()}";
            _delays.RemoveAt(0);
        }

        private float GetAverage()
        {
            float result = 0;

            foreach (float delay in _delays)
                result += delay;

            return result / _delays.Count;
        }

        private float GetFramerate() =>
            1 / GetAverage();
    }
}