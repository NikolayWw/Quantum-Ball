using CodeBase.StaticData.Finish;
using UnityEngine;

namespace CodeBase.Finish
{
    public class FinishPlayerDetected : MonoBehaviour
    {
        [SerializeField] private FinishAnimation _finishAnimation;
        [SerializeField] private SphereCollider _findPlayerCollider;

        public void Construct(FinishConfig config)
        {
            _findPlayerCollider.radius = config.FindRadius;
        }

        private void OnTriggerEnter(Collider other)
        {
            Rigidbody attachedRigidbody = other.attachedRigidbody;
            if (attachedRigidbody != null && other.attachedRigidbody.gameObject.CompareTag("Player"))
            {
                _finishAnimation.PlayOpenDoor();
            }
        }
    }
}