using System;
using UnityEngine;

namespace CodeBase.StaticData.Obstacle
{
    [Serializable]
    public class ObstacleConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public ObstacleId Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }

        public void OnValidate()
        {
            _inspectorName = Id.ToString();
        }
    }
}