using System;
using System.Collections.Generic;

namespace CodeBase.StaticData.Obstacle.SpawnData
{
    [Serializable]
    public class ObstacleSpawnDataContainer
    {
        public List<ObstacleSpawnData> SpawnData;

        public void OnValidate()
        {
            SpawnData.ForEach(x => x.OnValidate());
        }
    }
}