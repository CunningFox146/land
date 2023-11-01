using Land.Utils;
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

        public virtual void Show()
        {
            uiDocument.rootVisualElement.Show();
        }

        public virtual void Hide()
        {
            uiDocument.rootVisualElement.Hide();
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