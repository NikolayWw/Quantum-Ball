using CodeBase.Player;
using CodeBase.StaticData.Finish;
using CodeBase.StaticData.Obstacle;
using CodeBase.StaticData.Platform;
using UnityEngine;

namespace CodeBase.Services.Factory
{
    public interface IGameFactory : IService
    {
        void CreateObstacle(ObstacleId id, Vector3 at);

        void CreatePlayer(Vector3 at);

        void CreatePlayerBall(Vector3 at, out PlayerBallMove move, out PlayerBallDetectedObstacle detected, out PlayerBallPumpUp pumpUp);

        void Cleanup();

        void CreatePlatform(PlatformId id, Vector3 at);
        void CreateFinish(FinishId id, Vector3 at);
    }
}