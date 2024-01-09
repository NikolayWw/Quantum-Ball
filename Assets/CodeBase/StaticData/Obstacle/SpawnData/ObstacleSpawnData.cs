using System;
using UnityEngine;

namespace CodeBase.StaticData.Obstacle.SpawnData
{
    [Serializable]
    public class ObstacleSpawnData
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public ObstacleId Id { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }

        public ObstacleSpawnData(ObstacleId id, Vector3 position)
        {
            Id = id;
            Position = position;
        }

        public void OnValidate()
        {
            _inspectorName = Id.ToString();
        }
    }
}