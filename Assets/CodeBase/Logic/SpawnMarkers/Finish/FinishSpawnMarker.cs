using CodeBase.StaticData.Finish;
using UnityEngine;

namespace CodeBase.Logic.SpawnMarkers.Finish
{
    public class FinishSpawnMarker : MonoBehaviour
    {
        [field: SerializeField] public FinishId Id { get; private set; }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 1);
        }
    }
}