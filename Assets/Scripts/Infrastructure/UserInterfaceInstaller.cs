using System.Collections.Generic;
using DefaultNamespace.Sound;
using Land.Localization;
using Land.UI;
using Land.UI.Main;
using Land.UI.Popup;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class UserInterfaceInstaller : MonoInstaller
    {
        [SerializeField] private MainViewPortrait _mainViewPortraitPrefab;
        [SerializeField] private MainViewLandscape _mainViewLandscapePrefab;
        [Space]
        [SerializeField] private PopupViewPortrait _popupViewPortraitPrefab;
        [SerializeField] private PopupViewLandscape _popupViewLandscapePrefab;
        [Space] [SerializeField] private ParticleSystem _coinsFx;
        
        [SerializeField] private List<LocalizationData> _locales;
        
        public override void InstallBindings()
        {
            Container.BindFactory<MainViewPortrait, MainViewPortrait.Factory>()
                .FromComponentInNewPrefab(_mainViewPortraitPrefab);
            Container.BindFactory<MainViewLandscape, MainViewLandscape.Factory>()
                .FromComponentInNewPrefab(_mainViewLandscapePrefab);
            
            Container.BindFactory<PopupViewPortrait, PopupViewPortrait.Factory>()
                .FromComponentInNewPrefab(_popupViewPortraitPrefab);
            Container.BindFactory<PopupViewLandscape, PopupViewLandscape.Factory>()
                .FromComponentInNewPrefab(_popupViewLandscapePrefab);
            
            Container.Bind<ParticleSystem>().FromInstance(_coinsFx).AsSingle();
            Container.BindInterfacesAndSelfTo<ViewService>().AsSingle();
            Container.Bind<IEnumerable<LocalizationData>>().FromInstance(_locales).AsSingle();
            Container.Bind<ILocalizationService>().To<LocalizationService>().AsSingle();
            Container.Bind<MainViewModel>().ToSelf().AsSingle();
            Container.Bind<PopupViewModel>().ToSelf().AsSingle();
            Container.Bind<ISoundsSystem>().To<SoundsSystem>().FromComponentsInHierarchy().AsSingle();
        }
    }
}