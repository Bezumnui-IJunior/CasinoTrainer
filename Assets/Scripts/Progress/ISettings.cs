namespace Progress
{
    public interface ISettings : IPersistant
    {
        float MusicVolume { get; set; }
        int TargetFrameRate { get; set; }
        int DefaultBet { get; set; }
        float SoundFXVolume { get; set; }
    }
}