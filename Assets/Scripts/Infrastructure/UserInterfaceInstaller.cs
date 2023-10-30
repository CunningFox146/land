using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Land.Localization;
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
        [SerializeField] private List<LocalizationData> _locales;
        
        public override void InstallBindings()
        {
            Container.BindFactory<MainViewPortrait, MainViewPortrait.Factory>()
                .FromComponentInNewPrefab(_mainViewPortraitPrefab);
            Container.BindFactory<MainViewLandscape, MainViewLandscape.Factory>()
                .FromComponentInNewPrefab(_mainViewLandscapePrefab);
            
            Container.BindInterfacesAndSelfTo<ViewService>().AsSingle();
            Container.Bind<IEnumerable<LocalizationData>>().FromInstance(_locales).AsSingle();
            Container.Bind<ILocalizationService>().To<LocalizationService>().AsSingle();
            Container.Bind<MainViewModel>().ToSelf().AsSingle();
        }
    }
}