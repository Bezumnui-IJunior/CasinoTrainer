using System;
using DG.Tweening;
using UnityEngine;

namespace View
{
    public class MenuPopInAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration = 0.2f;
        
        [SerializeField] private CanvasGroup _canvasGroup;
        

        private void Start()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, _duration);
        }
    }
}