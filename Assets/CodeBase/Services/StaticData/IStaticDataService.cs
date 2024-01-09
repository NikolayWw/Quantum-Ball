using CodeBase.StaticData.Finish;
using CodeBase.StaticData.Level;
using CodeBase.StaticData.Obstacle;
using CodeBase.StaticData.Platform;
using CodeBase.StaticData.Player;
using CodeBase.StaticData.Window;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();

        WindowStaticData WindowData { get; }
        PlayerStaticData PlayerStaticData { get; }
        LevelStaticData LevelStaticData { get; }
        ObstacleConfig ForObstacle(ObstacleId id);
        PlatformConfig ForPlatform(PlatformId id);
        FinishConfig ForFinish(FinishId id);
    }
}