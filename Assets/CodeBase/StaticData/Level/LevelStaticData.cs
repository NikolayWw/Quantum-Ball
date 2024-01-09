using CodeBase.StaticData.Finish.SpawnData;
using CodeBase.StaticData.Obstacle.SpawnData;
using CodeBase.StaticData.Platform.SpawnData;
using UnityEngine;

namespace CodeBase.StaticData.Level
{
    [CreateAssetMenu(menuName = "Static Data/Level Static Data", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        [field: SerializeField] public Vector3 PlayerInitialPosition { get; private set; }
        public ObstacleSpawnDataContainer ObstacleSpawnDataContainer;
        public PlatformSpawnData PlatformSpawnData;
        public FinishSpawnData FinishSpawnData;

        public void SetSpawnData(Vector3 playerInitialPosition, ObstacleSpawnDataContainer dataContainer, PlatformSpawnData platformSpawnData, FinishSpawnData finishSpawnData)
        {
            ObstacleSpawnDataContainer = dataContainer;
            PlayerInitialPosition = playerInitialPosition;
            PlatformSpawnData = platformSpawnData;
            FinishSpawnData = finishSpawnData;

            Validate();
        }

        private void OnValidate()
        {
            Validate();
        }

        private void Validate()
        {
            ObstacleSpawnDataContainer.OnValidate();
            PlatformSpawnData.OnValidate();
            FinishSpawnData.OnValidate();
        }
    }
}