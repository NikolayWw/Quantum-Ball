using CodeBase.Logic.SpawnMarkers.Finish;
using CodeBase.Logic.SpawnMarkers.Obstacle;
using CodeBase.Logic.SpawnMarkers.Platform;
using CodeBase.Logic.SpawnMarkers.Player;
using CodeBase.StaticData.Finish.SpawnData;
using CodeBase.StaticData.Level;
using CodeBase.StaticData.Obstacle.SpawnData;
using CodeBase.StaticData.Platform.SpawnData;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    public class CollectLevelDataEditor : MonoBehaviour
    {
        private const string LevelStaticDataPath = "Level/LevelStaticData";

        [MenuItem("Tools/Collect Level Data")]
        private static void CollectLevelData()
        {
            LevelStaticData levelData = Resources.Load<LevelStaticData>(LevelStaticDataPath);
            Vector3 playerInitialPoint = FindObjectOfType<PlayerSpawnMarker>().transform.position;

            PlatformSpawnMarker platformMarker = FindObjectOfType<PlatformSpawnMarker>();
            PlatformSpawnData platformSpawnData = new PlatformSpawnData(platformMarker.transform.position, platformMarker.Id);

            FinishSpawnMarker finishMarker = FindObjectOfType<FinishSpawnMarker>();
            FinishSpawnData finishSpawnData = new FinishSpawnData(finishMarker.transform.position, finishMarker.Id);

            levelData.SetSpawnData(playerInitialPoint, FindObstacleSpawnData(), platformSpawnData, finishSpawnData);
            EditorUtility.SetDirty(levelData);
        }

        private static ObstacleSpawnDataContainer FindObstacleSpawnData()
        {
            return new()
            {
                SpawnData = FindObjectsOfType<ObstacleSpawnMarker>()
                    .Select(x => new ObstacleSpawnData(x.Id, x.transform.position)).ToList()
            };
        }
    }
}