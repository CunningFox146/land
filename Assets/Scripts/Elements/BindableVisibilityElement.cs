using Land.Utils;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Extensions;
using UnityMvvmToolkit.Core.Interfaces;

namespace Land.Elements
{
    public class BindableVisibilityElement : VisualElement, IBindableElement
    {
        private PropertyBindingData _visibilityPathBindingData;
        private IProperty<bool> _visibilityProperty;
        
        private bool IsInverted { get; set; }

        public string BindingVisibilityPath { get; private set; }
        
        public void SetBindingContext(IBindingContext context, IObjectProvider objectProvider)
        {
            _visibilityPathBindingData ??= BindingVisibilityPath.ToPropertyBindingData();

            _visibilityProperty = objectProvider.RentProperty<bool>(context, _visibilityPathBindingData);
            _visibilityProperty.ValueChanged += OnVisibilityChanged;

            SetVisible(_visibilityProperty.Value);
        }

        public void ResetBindingContext(IObjectProvider objectProvider)
        {
            if (_visibilityProperty == null)
                return;
            
            _visibilityProperty.ValueChanged -= OnVisibilityChanged;
            objectProvider.ReturnProperty(_visibilityProperty);
            _visibilityProperty = null;
            
            SetVisible(false);
        }

        private void OnVisibilityChanged(object sender, bool newValue)
        {
            SetVisible(newValue);
        }

        private void SetVisible(bool newValue)
        {
            if (IsInverted)
                newValue = !newValue;
            
            if (newValue)
                this.Show();
            else
                this.Hide();
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlBoolAttributeDescription _isInverted = new()
                { name = "is-inverted", defaultValue = default };
            private readonly UxmlStringAttributeDescription _bindingImageAttribute = new()
                { name = "binding-visibility-path", defaultValue = string.Empty };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                var element = (BindableVisibilityElement)visualElement; 
                element.IsInverted = _isInverted.GetValueFromBag(bag, context);
                element.BindingVisibilityPath = _bindingImageAttribute.GetValueFromBag(bag, context);
            }
        }

        public new class UxmlFactory : UxmlFactory<BindableVisibilityElement, UxmlTraits> { }
    }
}