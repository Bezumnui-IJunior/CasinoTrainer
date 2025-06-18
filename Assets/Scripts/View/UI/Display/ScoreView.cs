using Features.BlackJack.Components;
using Scellecs.Morpeh;
using TMPro;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using View.Windows;

namespace View.UI.Display
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public abstract class ScoreView<TEntityTag> : MonoBehaviour, IScoreView where TEntityTag : struct, IComponent
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private string _prefix;
        private int _lastScore;
        private Filter _playerFilter;
        private Stash<ScoreComponent> _playerScore;

        private void Update()
        {
            foreach (Entity entity in _playerFilter)
            {
                ref int score = ref _playerScore.Get(entity).Value;

                if (score == _lastScore)
                    continue;

                _lastScore = score;
                UpdateScore();
            }
        }

        public void Initialize(World world)
        {
            _playerScore = world.GetStash<ScoreComponent>();

            _playerFilter = world.Filter
                .With<TEntityTag>()
                .With<ScoreComponent>()
                .Build();
        }
        
        private void UpdateScore()
        {
            _text.text = $"{_prefix} {_lastScore}";
        }
    }
}