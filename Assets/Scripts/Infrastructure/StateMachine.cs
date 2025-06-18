using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using VContainer;

namespace Infrastructure
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class StateMachine : IStateMachine, IStateChanger
    {
        private readonly IObjectResolver _resolver;
        private Dictionary<Type, IState> _states;
        private IState _currentState;

        public StateMachine(IObjectResolver resolver)
        {
            _resolver = resolver;
        }
        
        public void ChangeState<T>() where T: IState
        {
            _currentState?.Exit();
            _currentState = _resolver.Resolve<T>();
            _currentState?.Enter();
        }

        public void ClearState()
        {
            _currentState?.Exit();
            _currentState = null;
        }

        public void Dispose()
        {
            _resolver?.Dispose();
        }
    }
}