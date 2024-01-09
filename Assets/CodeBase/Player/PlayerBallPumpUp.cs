using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerBallPumpUp : MonoBehaviour
    {
        [SerializeField] private float _forwardOffset = 0.7f;
        [SerializeField] private Transform[] _targetsForPump;

        public void PumpUp(float value, Vector3 position)
        {
            foreach (Transform target in _targetsForPump)
                target.localScale += Vector3.one * value;
            
            Vector3 y = (transform.up * _targetsForPump[0].localScale.x / 2);
            Vector3 z = transform.forward * (_targetsForPump[0].localScale.x / 2);
            
            position += y + z;
            position.z += _forwardOffset;
            transform.position = position;
        }
    }
}