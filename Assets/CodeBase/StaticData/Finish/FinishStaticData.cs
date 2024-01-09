using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Finish
{
    [CreateAssetMenu(menuName = "Static Data/Finish Static Data", order = 0)]
    public class FinishStaticData : ScriptableObject
    {
        public List<FinishConfig> Configs;

        private void OnValidate()
        {
            Configs.ForEach(x => x.OnValidate());
        }
    }
}