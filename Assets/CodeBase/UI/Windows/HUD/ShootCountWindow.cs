using CodeBase.Services.Observers.PlayerObserver;
using CodeBase.Services.StaticData;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.HUD
{
    public class ShootCountWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _shootCounterText;
        private IPlayerObserverService _playerObserver;
        private int _currentCount;

        public void Construct(IStaticDataService dataService, IPlayerObserverService playerObserver)
        {
            _currentCount = dataService.PlayerStaticData.MaxShootCount + 1;//first refresh -1
            _playerObserver = playerObserver;
            _playerObserver.OnPlayerBallPresent += Refresh;
            Refresh();
        }

        private void OnDestroy()
        {
            _playerObserver.OnPlayerBallPresent -= Refresh;
        }

        private void Refresh()
        {
            _currentCount--;
            _shootCounterText.text = $"Shoot Counter {_currentCount}";
        }
    }
}