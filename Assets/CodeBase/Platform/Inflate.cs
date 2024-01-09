using System.Linq;
using CodeBase.Services.Observers.PlayerObserver;
using UnityEngine;

namespace CodeBase.Platform
{
    public class Inflate : MonoBehaviour
    {
        [SerializeField] private Transform[] _targets;
        [SerializeField] private bool _xSafe, _ySafe, _zSafe;
        private Vector3[] _startSizeTargets;
        private IPlayerObserverService _playerObserver;


        public void Construct(IPlayerObserverService playerObserver)
        {
            _playerObserver = playerObserver;
            _playerObserver.OnPlayerDeflates += Resize;

            _startSizeTargets = _targets.Select(x => x.localScale).ToArray();
        }

        private void OnDestroy()
        {
            _playerObserver.OnPlayerDeflates -= Resize;
        }

        private void Resize(float percent)
        {
            for (var i = 0; i < _targets.Length; i++)
            {
                Vector3 scale = _targets[i].localScale;
                Vector3 startScale = _startSizeTargets[i];
                scale.x = _xSafe ? startScale.x : Mathf.Lerp(0, startScale.x, percent);
                scale.y = _ySafe ? startScale.y : Mathf.Lerp(0, startScale.y, percent);
                scale.z = _zSafe ? startScale.z : Mathf.Lerp(0, startScale.z, percent);

                _targets[i].localScale = scale;
            }
        }
    }
}