using CodeBase.StaticData.Obstacle;
using UnityEngine;

namespace CodeBase.Logic.SpawnMarkers.Obstacle
{
    public class ObstacleSpawnMarker : MonoBehaviour
    {
        [field: SerializeField] public ObstacleId Id { get; private set; }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}