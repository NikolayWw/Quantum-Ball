using CodeBase.Data;
using CodeBase.Infrastructure.Logic;
using CodeBase.Services.Cleanup;
using CodeBase.Services.Factory;
using CodeBase.Services.GameOver;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Finish.SpawnData;
using CodeBase.StaticData.Level;
using CodeBase.StaticData.Obstacle.SpawnData;
using CodeBase.UI.Services.Factory;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IStaticDataService _dataService;
        private readonly ICleanupService _cleanupService;
        private readonly IGameOverService _gameOverService;

        public LoadLevelState(IGameStateMachine stateMachine, SceneLoader sceneLoader, LoadCurtain loadingCurtain, IGameFactory gameFactory,
            IUIFactory uiFactory, IStaticDataService dataService, ICleanupService cleanupService, IGameOverService gameOverService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _dataService = dataService;
            _cleanupService = cleanupService;
            _gameOverService = gameOverService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _cleanupService.Cleanup();

            if (CurrentSceneKey() == sceneName)
                _sceneLoader.Load(GameConstants.ReloadSceneKey, () => _sceneLoader.Load(sceneName, OnLoaded));
            else
                _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            LevelStaticData levelConfig = _dataService.LevelStaticData;

            _gameOverService.StartCheck(levelConfig);

            _uiFactory.CreateUIRoot();
            _uiFactory.CreateHUD();
            _uiFactory.CreateInputWindow();

            InitPlayer(levelConfig);
            InitObstacle(levelConfig);
            InitPlatform(levelConfig);
            InitFinish(levelConfig);

            _stateMachine.Enter<LoopState>();
        }

        private void InitFinish(LevelStaticData levelConfig)
        {
            FinishSpawnData finishData = levelConfig.FinishSpawnData;
            _gameFactory.CreateFinish(finishData.Id, finishData.Position);
        }

        private void InitPlayer(LevelStaticData levelConfig)
        {
            _gameFactory.CreatePlayer(levelConfig.PlayerInitialPosition);
        }

        private void InitPlatform(LevelStaticData levelConfig)
        {
            _gameFactory.CreatePlatform(levelConfig.PlatformSpawnData.Id, levelConfig.PlatformSpawnData.Position);
        }

        private void InitObstacle(LevelStaticData levelStaticData)
        {
            foreach (ObstacleSpawnData data in levelStaticData.ObstacleSpawnDataContainer.SpawnData)
                _gameFactory.CreateObstacle(data.Id, data.Position);
        }

        private static string CurrentSceneKey() =>
            SceneManager.GetActiveScene().name;
    }
}