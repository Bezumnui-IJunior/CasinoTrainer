using System;
using Newtonsoft.Json;

namespace Progress
{
    public class PlayerData : PersistantData, IPlayerData
    {
        private const string PrefsKey = "PlayerData";

        public PlayerData() : base(PrefsKey) { }

        [JsonProperty] public float PlayerMoney { get; private set; } = 1000f;

        public void ChargeMoney(float value)
        {
            if (value < 0 || value > PlayerMoney)
                throw new InvalidOperationException($"Invalid charge: {value}. Should be validated before.");

            PlayerMoney -= value;
        }

        public void AddMoney(float value)
        {
            if (value < 0)
                throw new InvalidOperationException($"Invalid add: {value}. Should be validated before.");

            PlayerMoney += value;
        }

        public static PlayerData LoadOrDefault()
        {
            if (TryLoad(out PlayerData result, PrefsKey))
                return result;

            return new PlayerData();
        }
    }
}