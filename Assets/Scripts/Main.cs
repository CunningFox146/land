using System;
using Land.UI;
using Land.UI.Main;
using UnityEngine;

namespace DefaultNamespace
{
    public class Main : MonoBehaviour
    {
        private IViewService _viewService;

        [Zenject.Inject]
        private void Constructor(IViewService viewService)
        {
            _viewService = viewService;
        }

        private void Awake()
        {
            _viewService.PushView<MainView>();
        }
    }
}