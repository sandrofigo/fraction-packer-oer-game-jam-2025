using Interaction;
using Zenject;

namespace Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ControlsManager>().FromNew().AsSingle();
        }
    }
}
