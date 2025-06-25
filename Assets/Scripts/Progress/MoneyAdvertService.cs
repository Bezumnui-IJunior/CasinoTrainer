using System;
using VContainer;

namespace Progress
{
    public class MoneyAdvertService : IMoneyAdvertService
    {
        private readonly IPlayerData _playerData;

        [Inject]
        public MoneyAdvertService(IPlayerData playerData)
        {
            _playerData = playerData;
        }

        public bool СanWatchAdvert()
        {
            if (_playerData.WhenCanWatchAdvert <= DateTime.Now)
                ResetCounter();

            return _playerData.AdvertWatchedCount < SettingsConstants.AdvertsCappingValue;
        }

        public void WatchAdvert()
        {
            _playerData.AdvertWatchedCount++;
        }

        public bool IsFirstAdvert() =>
            _playerData.AdvertWatchedCount == 1;

        private void ResetCounter()
        {
            _playerData.FirstAdvertWatched = DateTime.Now;
            _playerData.AdvertWatchedCount = 0;
        }
    }
}