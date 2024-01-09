using System;
using UnityEngine;

namespace CodeBase.StaticData.Platform
{
    [Serializable]
    public class PlatformConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public PlatformId Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }

        public void OnValidate()
        {
            _inspectorName = Id.ToString();
        }
    }
}