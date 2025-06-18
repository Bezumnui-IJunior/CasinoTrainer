using Unity.IL2CPP.CompilerServices;

namespace Infrastructure
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public abstract class State : IState, IStateChanger
    {
        private readonly IStateMachine _stateMachine;

        protected State(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }

        public void ChangeState<T>() where T : IState =>
            _stateMachine.ChangeState<T>();
    }
}