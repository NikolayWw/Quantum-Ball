using System;
using UnityEngine;

namespace CodeBase.StaticData.Finish
{
    [Serializable]
    public class FinishConfig
    {
        [SerializeField] private string _nspectorName;
        [field: SerializeField] public FinishId Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public float FindRadius { get; private set; } = 5;

        public void OnValidate()
        {
            _nspectorName = Id.ToString();
        }
    }
}