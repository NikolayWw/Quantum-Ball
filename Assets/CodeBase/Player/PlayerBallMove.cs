using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerBallMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        private PlayerStaticData _config;

        public void Construct(PlayerStaticData config)
        {
            _config = config;
        }

        public void Move(Vector3 direction)
        {
            if (_rigidbody != null)
                _rigidbody.AddForce(direction * _config.PlayerBallMoveSpeed, ForceMode.VelocityChange);
        }
    }
}