using Infrastructure;
using Progress;
using TMPro;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using VContainer;

namespace View.UI.Display
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class MoneyCounterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        private IPlayerData _playerData;

        private void Awake()
        {
            this.DoSelfInjection();
        }

        private void Update()
        {
            UpdateScore();
        }

        [Inject]
        private void Construct(IPlayerData data)
        {
            _playerData = data;
        }

        private void UpdateScore()
        {
            _text.text = $"${_playerData.PlayerMoney}";
        }
    }
}