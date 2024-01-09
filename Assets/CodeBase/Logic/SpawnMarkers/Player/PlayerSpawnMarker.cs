using UnityEngine;

namespace CodeBase.Logic.SpawnMarkers.Player
{
    public class PlayerSpawnMarker : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}