using CodeBase.StaticData.Finish;
using CodeBase.StaticData.Level;
using CodeBase.StaticData.Obstacle;
using CodeBase.StaticData.Platform;
using CodeBase.StaticData.Player;
using CodeBase.StaticData.Window;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string WindowStaticDataPath = "Windows/WindowStaticData";
        private const string ObstacleDataPath = "Obstacle/ObstacleStaticData";
        private const string PlayerDataPath = "Player/PlayerStaticData";
        private const string LevelDataPath = "Level/LevelStaticData";
        private const string PlatformDataPath = "Platform/PlatformStaticData";
        private const string FinishDataPath = "Finish/FinishStaticData";

        public WindowStaticData WindowData { get; private set; }
        public PlayerStaticData PlayerStaticData { get; private set; }
        public LevelStaticData LevelStaticData { get; private set; }

        private Dictionary<ObstacleId, ObstacleConfig> _obstacleConfigs;
        private Dictionary<PlatformId, PlatformConfig> _platformConfigs;
        private Dictionary<FinishId, FinishConfig> _finishConfigs;

        public void Load()
        {
            LevelStaticData = Resources.Load<LevelStaticData>(LevelDataPath);
            WindowData = Resources.Load<WindowStaticData>(WindowStaticDataPath);
            PlayerStaticData = Resources.Load<PlayerStaticData>(PlayerDataPath);

            _obstacleConfigs = Resources.Load<ObstacleStaticData>(ObstacleDataPath).ObstacleConfigs
                .ToDictionary(x => x.Id, x => x);

            _platformConfigs = Resources.Load<PlatformStaticData>(PlatformDataPath).Configs
                .ToDictionary(x => x.Id, x => x);

            _finishConfigs = Resources.Load<FinishStaticData>(FinishDataPath).Configs.ToDictionary(x => x.Id, x => x);
        }

        public ObstacleConfig ForObstacle(ObstacleId id) =>
            _obstacleConfigs.TryGetValue(id, out ObstacleConfig cfg) ? cfg : null;

        public PlatformConfig ForPlatform(PlatformId id) =>
            _platformConfigs.TryGetValue(id, out PlatformConfig cfg) ? cfg : null;

        public FinishConfig ForFinish(FinishId id) =>
            _finishConfigs.TryGetValue(id, out FinishConfig cfg) ? cfg : null;
    }
}