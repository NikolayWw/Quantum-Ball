using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Services.Observers.PlayerObserver;
using CodeBase.Services.StaticData;
using CodeBase.UI.Windows.HUD;
using CodeBase.UI.Windows.InputWindow;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private Transform _uiRoot;
        private readonly AllServices _allServices;

        public UIFactory(AllServices allServices)
        {
            _allServices = allServices;
        }

        public void CreateUIRoot()
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            _uiRoot = Object.Instantiate(dataService.WindowData.UiRootPrefab).transform;
        }

        public void CreateHUD()
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            IPlayerObserverService playerObserverService = GetService<IPlayerObserverService>();
            GameObject prefab = dataService.WindowData.HUDPrefab;

            GameObject hudInstance = Object.Instantiate(prefab, _uiRoot);
            hudInstance.GetComponentInChildren<ShootCountWindow>().Construct(dataService, playerObserverService);
        }

        public void CreateInputWindow()
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            IInputService inputService = GetService<IInputService>();

            InputWindow prefab = dataService.WindowData.InputWindowPrefab;
            InputWindow instance = Object.Instantiate(prefab, _uiRoot);
            instance.Construct(inputService);
        }

        public void CreateWinWindow()
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            GameObject prefab = dataService.WindowData.WinWindowPrefab;
            Object.Instantiate(prefab, _uiRoot);
        }

        public void CreateLoseWindow()
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            GameObject prefab = dataService.WindowData.LoseWindowPrefab;
            Object.Instantiate(prefab, _uiRoot);
        }

        private TService GetService<TService>() where TService : IService =>
            _allServices.Single<TService>();
    }
}