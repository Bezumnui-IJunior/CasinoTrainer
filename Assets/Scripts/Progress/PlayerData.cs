using Newtonsoft.Json;

namespace Progress
{
    public class PlayerData : PersistantData, IPlayerData
    {
        private const string PrefsKey = "PlayerData";

        public PlayerData() : base(PrefsKey) { }

        [JsonProperty] public float PlayerMoney { get; set; } = 1000f;

        public static PlayerData LoadOrDefault()
        {
            if (TryLoad(out PlayerData result, PrefsKey))
                return result;

            return new PlayerData();
        }
    }
}