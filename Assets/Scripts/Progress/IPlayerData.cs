using System;

namespace Progress
{
    public interface IPlayerData
    {
        float PlayerMoney { get; set; }
        DateTime LastBonusReceived { get; set; }
        DateTime WhenCanReceiveBonus { get; }
        DateTime FirstAdvertWatched { get; set; }
        DateTime WhenCanWatchAdvert { get; }
        int AdvertWatchedCount { get; set; }
        void Save();
    }
}