using DefaultNamespace;
using Land.UI;
using Land.UI.Main;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class UserInterfaceInstaller : MonoInstaller
    {
        [SerializeField] private MainViewPortrait _mainViewPortraitPrefab;
        [SerializeField] private MainViewLandscape _mainViewLandscapePrefab;
        
        public override void InstallBindings()
        {
            Container.BindFactory<MainViewPortrait, MainViewPortrait.Factory>()
                .FromComponentInNewPrefab(_mainViewPortraitPrefab);
            Container.BindFactory<MainViewLandscape, MainViewLandscape.Factory>()
                .FromComponentInNewPrefab(_mainViewLandscapePrefab);
            
            Container.BindInterfacesAndSelfTo<ViewService>().AsSingle();
            Container.Bind<MainViewModel>().ToSelf().AsSingle();
        }
    }
}