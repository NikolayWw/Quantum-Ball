using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Obstacle
{
    [CreateAssetMenu(menuName = "Static Data/Obstacle Static Data", order = 0)]
    public class ObstacleStaticData : ScriptableObject
    {
        public List<ObstacleConfig> ObstacleConfigs;

        private void OnValidate()
        {
            ObstacleConfigs.ForEach(x => x.OnValidate());
        }
    }
}