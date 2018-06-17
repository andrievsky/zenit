using Code.Game;
using Code.Logger;
using Zenject;

namespace Code.Input
{
    public class InputInstaller : IAutoInstaller
    {
        public void InstallBindings(DiContainer container)
        {
            container.Bind<ITickable>().To<InputController>().AsSingle();
        }
    }

    public class InputController : ITickable
    {
        private ILog _logger;

        public InputController(ILog logger)
        {
            _logger = logger;
        }

        public void Tick()
        {
            if (!UnityEngine.Input.anyKeyDown)
            {
                return;
            }
            _logger.Log("Input Activated");
        }
    }
}