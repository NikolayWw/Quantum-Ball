using UnityEngine;

namespace CodeBase.Finish
{
    public class FinishAnimation : MonoBehaviour
    {
        private readonly int OpenDoorHash = Animator.StringToHash("Open Door");

        [SerializeField] private Animator _animator;

        public void PlayOpenDoor()
        {
            _animator.Play(OpenDoorHash);
        }
    }
}