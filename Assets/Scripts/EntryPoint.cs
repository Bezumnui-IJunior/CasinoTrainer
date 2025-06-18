using DG.Tweening;
using GameStates;
using Infrastructure;
using Progress;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EntryPoint : IStartable
{
    private readonly IStateMachine _stateMachine;
    private readonly IObjectResolver _resolver;
    private readonly ISettings _settings;

    [Inject]
    public EntryPoint(IStateMachine stateMachine, IObjectResolver resolver, ISettings settings)
    {
        _stateMachine = stateMachine;
        _resolver = resolver;
        _settings = settings;
    }

    public void Start()
    {
        _resolver.Resolve<GlobalInjector>();

        if (_settings.Load() == false)
            _settings.Save();

        DOTween.Init();
        Application.targetFrameRate = _settings.TargetFrameRate;
        _settings.Save();
        _stateMachine.ChangeState<BootstrapState>();
    }
}