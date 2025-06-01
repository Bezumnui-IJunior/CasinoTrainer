namespace Infrastructure
{
    public interface IFeatureFactory
    {
        T Create<T>();
    }
}