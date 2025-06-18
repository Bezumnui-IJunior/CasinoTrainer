namespace Infrastructure
{
    public interface IState
    {
        void Enter();
        void Update();
        void Exit();
    }
}