using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;

namespace Land.Views
{
    public class ViewManager : MonoBehaviour
    {
        private readonly Stack<ViewData> _views = new ();
        [SerializeField] private List<View> _allViews;
        [SerializeField] private PanelSettings _portraitSettings;
        [SerializeField] private PanelSettings _landscapeSettings;

        private ScreenOrientation Orientation { get; set; }

        private void Awake()
        {
            PushView<MainView>();
        }

        public void PushView<TView>()
        {
            var view = _allViews.FirstOrDefault(v => v.GetType() == typeof(TView));
            if (view is null)
                return;

            var viewComponent = new GameObject(typeof(TView).Name).AddComponent<UIDocument>();
            viewComponent.transform.SetParent(transform);
            _views.Push(new ViewData
            {
                View = view,
                Document = viewComponent
            });
            UpdateViews();
        }

        public void PopView()
        {
            var view = _views.Pop();
            Destroy(view.Document.gameObject);
        }

        private void Update()
        {
            var oldOrientation = Orientation;
            UpdateScreenRatio();
            
            if (oldOrientation != Orientation)
                UpdateViews();
        }

        private void UpdateViews()
        {
            foreach (var view in _views)
            {
                view.Document.visualTreeAsset = Orientation == ScreenOrientation.Portrait
                    ? view.View.PortraitView
                    : view.View.LandscapeView;

                view.Document.panelSettings = Orientation == ScreenOrientation.Portrait
                    ? _portraitSettings
                    : _landscapeSettings;
            }
        }

        private void UpdateScreenRatio()
        {
            Orientation = Screen.width > Screen.height ? ScreenOrientation.Landscape : ScreenOrientation.Portrait;
        }
        
        private enum ScreenOrientation
        {
            Portrait,
            Landscape
        }

        private struct ViewData
        {
            public View View { get; set; }
            public UIDocument Document { get; set; }
        }
    }
}