using System;
using Land.UI;
using Land.UI.Main;
using Land.UI.Popup;
using UnityEngine;

namespace DefaultNamespace
{
    public class Main : MonoBehaviour
    {
        private IViewService _viewService;
        private ParticleSystem _coinsFx;

        [Zenject.Inject]
        private void Constructor(IViewService viewService, MainViewModel mainViewModel, ParticleSystem coinsFx)
        {
            _coinsFx = coinsFx;
            _viewService = viewService;
            mainViewModel.AnimDone += OnSpinAnimDone;
        }

        private void OnSpinAnimDone()
        {
            _viewService.PushView<PopupView>();
            _coinsFx.gameObject.SetActive(true);
        }

        private void Awake()
        {
            Application.targetFrameRate = 300;
            _viewService.PushView<MainView>();
        }
    }
}