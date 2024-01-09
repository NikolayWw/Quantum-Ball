using CodeBase.Services;

namespace CodeBase.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateUIRoot();
        void CreateInputWindow();
        void CreateWinWindow();
        void CreateLoseWindow();
        void CreateHUD();
    }
}