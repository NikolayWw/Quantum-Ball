using System;
using UnityEngine;

namespace CodeBase.StaticData.Platform.SpawnData
{
    [Serializable]
    public class PlatformSpawnData
    {
        [SerializeField] private string _inspectorName;

        [field: SerializeField] public PlatformId Id { get; private set; }

        [field: SerializeField] public Vector3 Position { get; private set; }

        public PlatformSpawnData(Vector3 position, PlatformId id)
        {
            Position = position;
            Id = id;
        }

        public void OnValidate()
        {
            _inspectorName = Id.ToString();
        }
    }
}