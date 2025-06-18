namespace Progress
{
    public interface IPlayerData
    {
        float PlayerMoney { get; }
        void Save();
        void ChargeMoney(float value);
        void AddMoney(float value);
    }
}