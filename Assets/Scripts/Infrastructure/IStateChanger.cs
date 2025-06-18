namespace Infrastructure
{
    public interface IStateChanger
    {
        void ChangeState<T>() where T : IState;
    }
}