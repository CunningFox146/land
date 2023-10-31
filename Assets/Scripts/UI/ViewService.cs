using System;
using System.Collections.Generic;
using Land.UI.Main;
using Land.UI.Popup;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Land.UI
{
    public class ViewService : ITickable, IViewService
    {
        private readonly Stack<ViewData> _views = new ();

        private readonly Dictionary<Type, (IView portraitView, IView landscapeView)> _allViews;

        private ScreenOrientation Orientation { get; set; }

        public ViewService(MainViewLandscape.Factory mainViewLandscapeFactory,  MainViewPortrait.Factory mainViewPortraitFactory, PopupViewPortrait.Factory popupViewPortrait, PopupViewLandscape.Factory popupViewLandscape)
        {
            _allViews = new Dictionary<Type, (IView portraitView, IView landscapeView)>
            {
                [typeof(MainView)] = (mainViewLandscapeFactory.Create(), mainViewPortraitFactory.Create()),
                [typeof(PopupView)] = (popupViewLandscape.Create(), popupViewPortrait.Create())
            };
            foreach (var viewData in _allViews.Values)
            {
                viewData.portraitView.Hide();
                viewData.landscapeView.Hide();
            }
        }

        public void PushView<TView>()
        {
            var viewData = _allViews[typeof(TView)];

            var sortOrder = _views.Count;
            
            viewData.portraitView.SortOrder = sortOrder;
            viewData.landscapeView.SortOrder = sortOrder;
                
            _views.Push(new ViewData
            {
                PortraitView = viewData.portraitView,
                LandscapeView = viewData.landscapeView,
            });
            UpdateViews();
        }

        public void PopView()
        {
            var view = _views.Pop();
            view.Hide();
        }

        public void Tick()
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
                if (Orientation == ScreenOrientation.Portrait)
                {
                    view.PortraitView.Show();
                    view.LandscapeView.Hide();
                }
                else
                {
                    view.PortraitView.Hide();
                    view.LandscapeView.Show();
                }
            }
        }

        private void UpdateScreenRatio()
        {
            Orientation = Screen.width > Screen.height ? ScreenOrientation.Portrait : ScreenOrientation.Landscape;
        }
        
        private enum ScreenOrientation
        {
            Portrait,
            Landscape
        }

        private struct ViewData
        {
            public IView PortraitView {get; set; }
            public IView LandscapeView {get; set; }

            public void Hide()
            {
                PortraitView.Hide();
                LandscapeView.Hide();
                PortraitView.SortOrder = -1f;
                LandscapeView.SortOrder = -1f;
            }
        }
    }
}