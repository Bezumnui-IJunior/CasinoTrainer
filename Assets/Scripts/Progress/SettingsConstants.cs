using System;

namespace Progress
{
    public static class SettingsConstants
    {
        public static readonly int AddBonusFrequencySeconds = (int) TimeSpan.FromHours(8).TotalSeconds;
        public const string NotificationName = "Blackjack Trainer 3D";
        public const int AddBonusValue = 500;
        public const int AdvertsCappingValue = 3;
        public static readonly int AdvertsCappingSeconds = (int) TimeSpan.FromHours(1).TotalSeconds;
    }
}