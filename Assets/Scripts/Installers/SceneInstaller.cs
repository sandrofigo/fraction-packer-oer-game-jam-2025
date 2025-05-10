using Interaction;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField]
        private FractionBlockInteractor _fractionBlockInteractor;
        
        public override void InstallBindings()
        {
            Container.Bind<SlotGroupComponent>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<FractionBlockInteractor>().FromNewComponentOnNewPrefab(_fractionBlockInteractor).AsSingle().NonLazy();
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}
