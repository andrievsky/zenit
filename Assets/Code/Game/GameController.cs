using System;
using Code.Logger;
using Zenject;

namespace Code.Game
{
    public class GameAutoInstaller : IAutoInstaller
    {
        public void InstallBindings(DiContainer container)
        {
            container.Bind<IInitializable>().To<GameController>().AsSingle();
        }
    }
    
    public class GameController : IInitializable, IDisposable
    {
        private readonly ILog _logger;

        public GameController(ILog logger)
        {
            _logger = logger;
        }

        public void Initialize()
        {
            _logger.Log("GameController Ready");
        }

        public void Dispose()
        {
            _logger.Log("GameController Disposed");
        }
    }
}