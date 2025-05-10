using UI;
using Zenject;

namespace Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SlotGroupComponent>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}
