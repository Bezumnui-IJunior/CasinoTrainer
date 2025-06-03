namespace Features.BlackJack.Configs
{
    public interface IDealerConfig
    {
        float FirstCardTimeout { get; }
        float TakeCardTimeout { get; }
    }
}