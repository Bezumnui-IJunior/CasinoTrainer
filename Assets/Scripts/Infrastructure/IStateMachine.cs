namespace Infrastructure
{
    public interface IStateMachine : IStateChanger
    {
        void ClearState();
    }
}