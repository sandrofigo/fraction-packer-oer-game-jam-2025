using Fractions;
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

        [SerializeField]
        private LevelBuilder _levelBuilder;

        [SerializeField]
        private FractionBuilder _fractionBuilder;
        
        public override void InstallBindings()
        {
            // Building
            Container.Bind<SlotGroupComponent>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<FractionBuilder>().FromComponentInNewPrefab(_fractionBuilder).AsSingle();
            Container.Bind<LevelBuilder>().FromComponentInNewPrefab(_levelBuilder).AsSingle().NonLazy();
            
            // Interaction
            Container.Bind<FractionBlockInteractor>().FromComponentInNewPrefab(_fractionBlockInteractor).AsSingle().NonLazy();
        }
    }
}
