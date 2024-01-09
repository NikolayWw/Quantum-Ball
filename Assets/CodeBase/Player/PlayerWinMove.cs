using CodeBase.Services.Observers;
using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerWinMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private IGameObserverService _observerService;
        private PlayerStaticData _config;
        private bool _isPlayerWin;

        public void Construct(PlayerStaticData config, IGameObserverService observerService)
        {
            _config = config;
            _observerService = observerService;

            _observerService.OnGameWin += PlayerWin;
        }

        private void OnDestroy()
        {
            _observerService.OnGameWin -= PlayerWin;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (_isPlayerWin == false)
                return;

            Jump();
        }

        private void FixedUpdate()
        {
            if (_isPlayerWin == false)
                return;

            UpdateMove();
        }

        private void PlayerWin()
        {
            _rigidbody.isKinematic = false;
            _isPlayerWin = true;
            Jump();
        }

        private void Jump()
        {
            Vector3 velocity = _rigidbody.velocity;
            velocity = new Vector3(velocity.x, 0, velocity.z);
            _rigidbody.velocity = velocity;
            _rigidbody.AddForce(Vector3.up * _config.WinJumpForce, ForceMode.VelocityChange);
        }

        private void UpdateMove()
        {
            float y = _rigidbody.velocity.y;
            Vector3 velocity = (transform.forward * _config.WinForwardMoveSpeed);
            velocity.y = y;
            _rigidbody.velocity = velocity;
        }
    }
}