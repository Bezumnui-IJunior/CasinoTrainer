using System;
using Newtonsoft.Json;

namespace Progress
{
    public class PlayerData : PersistantData, IPlayerData
    {
        private const string PrefsKey = "PlayerData";

        public PlayerData() : base(PrefsKey) { }

        [JsonProperty] public float PlayerMoney { get; set; } = 1000f;
        [JsonProperty] public DateTime LastBonusReceived { get; set; } = DateTime.MinValue;
        [JsonProperty] public DateTime FirstAdvertWatched { get; set; } = DateTime.MinValue;
        [JsonProperty] public int AdvertWatchedCount { get; set; } = 0;
        public DateTime WhenCanReceiveBonus => LastBonusReceived.Add(TimeSpan.FromSeconds(SettingsConstants.AddBonusFrequencySeconds));
        public DateTime WhenCanWatchAdvert => FirstAdvertWatched.Add(TimeSpan.FromSeconds(SettingsConstants.AdvertsCappingSeconds));

        public static PlayerData LoadOrDefault()
        {
            if (TryLoad(out PlayerData result, PrefsKey))
                return result;

            return new PlayerData();
        }
    }
}