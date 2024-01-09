using CodeBase.Finish;
using CodeBase.Obstacle;
using CodeBase.Platform;
using CodeBase.Player;
using CodeBase.Services.Input;
using CodeBase.Services.Observers;
using CodeBase.Services.Observers.PlayerObserver;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Finish;
using CodeBase.StaticData.Obstacle;
using CodeBase.StaticData.Platform;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly AllServices _allServices;
        private readonly List<Collider> _ignorePlayerBallColliders = new();

        public GameFactory(AllServices allServices)
        {
            _allServices = allServices;
        }

        public void Cleanup()
        {
            _ignorePlayerBallColliders.Clear();
        }

        public void CreateObstacle(ObstacleId id, Vector3 at)
        {
            IStaticDataService staticData = GetService<IStaticDataService>();
            IGameObserverService observer = GetService<IGameObserverService>();
            ObstacleConfig config = staticData.ForObstacle(id);

            GameObject instance = Object.Instantiate(config.Prefab, at, Quaternion.Euler(0, Random.Range(-360, 360), 0));
            instance.GetComponent<ObstacleCollisionHandler>().Construct(observer);
        }

        public void CreatePlatform(PlatformId id, Vector3 at)
        {
            IStaticDataService staticData = GetService<IStaticDataService>();
            IPlayerObserverService playerObserverService = GetService<IPlayerObserverService>();
            PlatformConfig config = staticData.ForPlatform(id);

            GameObject instance = Object.Instantiate(config.Prefab, at, Quaternion.identity);
            instance.GetComponent<Inflate>().Construct(playerObserverService);
            AddPlayerBallCollision(instance);
        }

        public void CreateFinish(FinishId id, Vector3 at)
        {
            IStaticDataService staticData = GetService<IStaticDataService>();
            FinishConfig config = staticData.ForFinish(id);

            GameObject instance = Object.Instantiate(config.Prefab, at, Quaternion.identity);
            instance.GetComponentInChildren<FinishPlayerDetected>().Construct(config);
        }

        public void CreatePlayer(Vector3 at)
        {
            IStaticDataService staticData = GetService<IStaticDataService>();
            IGameObserverService gameObserverService = GetService<IGameObserverService>();
            IPlayerObserverService playerObserverService = GetService<IPlayerObserverService>();
            IInputService inputService = GetService<IInputService>();
            GameObject playerPrefab = staticData.PlayerStaticData.PlayerPrefab;

            GameObject playerInstance = Object.Instantiate(playerPrefab, at, Quaternion.identity);
            playerInstance.GetComponent<PlayerDeflates>().Construct(playerObserverService, gameObserverService, this, inputService, staticData.PlayerStaticData);
            playerInstance.GetComponent<PlayerCheckLose>().Construct(staticData.PlayerStaticData, playerObserverService);
            playerInstance.GetComponent<PlayerWinMove>().Construct(staticData.PlayerStaticData, gameObserverService);
            AddPlayerBallCollision(playerInstance);
        }

        public void CreatePlayerBall(Vector3 at, out PlayerBallMove move, out PlayerBallDetectedObstacle detected, out PlayerBallPumpUp pumpUp)
        {
            IStaticDataService staticData = GetService<IStaticDataService>();
            IGameObserverService gameObserver = GetService<IGameObserverService>();
            IPlayerObserverService playerObserverService = GetService<IPlayerObserverService>();

            GameObject prefab = staticData.PlayerStaticData.PlayerBallPrefab;

            GameObject instance = Object.Instantiate(prefab, at, Quaternion.identity);
            detected = instance.GetComponent<PlayerBallDetectedObstacle>();
            pumpUp = instance.GetComponent<PlayerBallPumpUp>();
            move = instance.GetComponent<PlayerBallMove>();

            detected.Construct(playerObserverService, gameObserver, staticData.PlayerStaticData);
            move.Construct(staticData.PlayerStaticData);

            IgnorePlayerBallCollision(instance);
        }

        private void AddPlayerBallCollision(GameObject arg)
        {
            Collider[] colliders = arg.GetComponentsInChildren<Collider>();
            foreach (Collider argCollider in colliders)
            {
                _ignorePlayerBallColliders.Add(argCollider);
            }
        }

        private void IgnorePlayerBallCollision(GameObject playerBall)
        {
            Collider[] colliders = playerBall.GetComponentsInChildren<Collider>();
            foreach (Collider argCollider in colliders)
            {
                foreach (Collider ballCollider in _ignorePlayerBallColliders)
                {
                    Physics.IgnoreCollision(ballCollider, argCollider);
                }
            }
        }

        private TService GetService<TService>() where TService : IService =>
            _allServices.Single<TService>();
    }
}