using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Platform
{
    [CreateAssetMenu(menuName = "Static Data/Platform Static Data", order = 0)]
    public class PlatformStaticData : ScriptableObject
    {
        public List<PlatformConfig> Configs;

        private void OnValidate()
        {
            Configs.ForEach(x => x.OnValidate());
        }
    }
}