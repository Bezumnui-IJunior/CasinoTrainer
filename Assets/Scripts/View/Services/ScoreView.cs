using TMPro;
using UnityEngine;

namespace View.Services
{
    public class ScoreView : MonoBehaviour, IScoreView
    {
        [SerializeField] private TextMeshProUGUI _textMeshPro;

        public void UpdateScore(int score)
        {
            _textMeshPro.text = $"Player Score: {score}";
        }
    }
}