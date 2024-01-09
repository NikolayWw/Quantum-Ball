using System;
using UnityEngine;

namespace CodeBase.StaticData.Finish.SpawnData
{
    [Serializable]
    public class FinishSpawnData
    {
        [SerializeField] private string _inspectorName;

        [field: SerializeField] public FinishId Id { get; private set; }

        [field: SerializeField] public Vector3 Position { get; private set; }

        public FinishSpawnData(Vector3 position, FinishId id)
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