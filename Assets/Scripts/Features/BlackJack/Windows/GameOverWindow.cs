using System;
using Windows;
using DG.Tweening;
using GameStates;
using Infrastructure;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Features.BlackJack.Windows
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    public class GameOverWindow : Window
    {
        private const float ZeroTransparency = 0;
        private const float FullTransparency = 1;

        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Vector2 _inPivot = new Vector2(0.5f, -1);
        [SerializeField] private Vector2 _workingPivot = new Vector2(0.5f, 0.5f);
        [SerializeField] private Vector2 _outPivot = new Vector2(0.5f, 1);
        [SerializeField] private float _secondsToEase = 0.3f;
        [SerializeField] private float _delaySeconds = 0.8f;
        private CanvasGroup _canvasGroup;

        private RectTransform _rectTransform;
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        protected override void OnAwake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();

            if (_restartButton == null)
                throw new NullReferenceException($"{nameof(_restartButton)} is unassigned");
        }

        protected override void Initialize()
        {
            _restartButton.onClick.AddListener(OnRestartClicked);
            _exitButton.onClick.AddListener(OnExitClicked);
            _rectTransform.pivot = _inPivot;
            _canvasGroup.alpha = ZeroTransparency;

            _rectTransform.DOPivot(_workingPivot, _secondsToEase).SetDelay(_delaySeconds);
            _canvasGroup.DOFade(FullTransparency, _secondsToEase).SetDelay(_delaySeconds);
        }

        protected override void Deinitialize()
        {
            _restartButton.onClick.RemoveListener(OnRestartClicked);
            _exitButton.onClick.RemoveListener(OnExitClicked);
            DOTween.Kill(_rectTransform);
            DOTween.Kill(_canvasGroup);
        }

        private void ChangeStateWithDestroy<T>() where T : IState
        {
            _stateMachine.ChangeState<T>();
            Destroy();
        }

        private void OnRestartClicked()
        {
            _canvasGroup.interactable = false;
            EaseOut(ChangeStateWithDestroy<BlackJackRunningState>);
        }

        private void OnExitClicked()
        {
            _canvasGroup.interactable = false;
            _stateMachine.ChangeState<MainMenuState>();
        }

        private void EaseOut(TweenCallback onComplete)
        {
            _rectTransform.DOPivot(_outPivot, _secondsToEase);

            _canvasGroup.DOFade(ZeroTransparency, _secondsToEase)
                .OnComplete(onComplete);
        }
    }
}