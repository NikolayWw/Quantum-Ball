using CodeBase.StaticData.Platform;
using UnityEngine;

namespace CodeBase.Logic.SpawnMarkers.Platform
{
    public class PlatformSpawnMarker : MonoBehaviour
    {
        [field: SerializeField] public PlatformId Id { get; private set; }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}