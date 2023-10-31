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

        [Zenject.Inject]
        private void Constructor(IViewService viewService, MainViewModel mainViewModel)
        {
            _viewService = viewService;
            mainViewModel.AnimDone += () => _viewService.PushView<PopupView>();
        }

        private void Awake()
        {
            _viewService.PushView<MainView>();
        }
    }
}