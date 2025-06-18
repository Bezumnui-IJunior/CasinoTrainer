namespace Progress
{
    public interface IPersistant
    {
        void Save();
        bool Load();
    }
}