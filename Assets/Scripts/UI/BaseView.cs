using UnityEngine.UIElements;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UITK;
using Zenject;

namespace Land.UI
{
    public class BaseView<TViewModel> : DocumentView<TViewModel>, IView
        where TViewModel : class, IBindingContext
    {
        private TViewModel _viewModel;
        protected UIDocument uiDocument;

        public float SortOrder
        {
            get => uiDocument.sortingOrder;
            set => uiDocument.sortingOrder = value;
        }

        public void Show()
        {
            uiDocument.rootVisualElement.style.visibility = Visibility.Hidden;
            uiDocument.rootVisualElement.style.display = DisplayStyle.None;
        }

        public void Hide()
        {
            uiDocument.rootVisualElement.style.visibility = Visibility.Visible;
            uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        [Inject]
        private void Constructor(TViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        protected override void OnInit()
        {
            base.OnInit();
            uiDocument = GetComponent<UIDocument>();
        }

        protected override TViewModel GetBindingContext()
        {
            return _viewModel;
        }
    }
}