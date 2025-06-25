using System;
using Newtonsoft.Json;
using TriInspector;
using UnityEngine;

namespace Progress
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Configs / Settings", order = 51)]
    public class Settings : PersistantScriptableObject, ISettings
    {
        protected override string PrefsKey => "Settings";

        [ShowInInspector] [JsonProperty] public float MusicVolume { get; set; } = 1f;
        [ShowInInspector] [JsonProperty] public int TargetFrameRate { get; set; } = 60;
        [ShowInInspector] [JsonProperty] public int DefaultBet { get; set; } = 20;

        public bool Load() =>
            Load<Settings>();
    }
}