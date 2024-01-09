using CodeBase.StaticData.Level;

namespace CodeBase.Services.GameOver
{
    public interface IGameOverService : IService
    {
        void StartCheck(LevelStaticData levelConfig);
        void Cleanup();
    }
}