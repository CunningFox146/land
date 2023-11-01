using Land.Utils;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Extensions;
using UnityMvvmToolkit.Core.Interfaces;

namespace Land.Elements
{
    public class BindableRotationElement : VisualElement, IBindableElement
    {
        private PropertyBindingData _rotationPathBindingData;
        private IProperty<float> _rotationProperty;
        

        public string BindingRotationPath { get; private set; }
        
        public void SetBindingContext(IBindingContext context, IObjectProvider objectProvider)
        {
            _rotationPathBindingData ??= BindingRotationPath.ToPropertyBindingData();

            _rotationProperty = objectProvider.RentProperty<float>(context, _rotationPathBindingData);
            _rotationProperty.ValueChanged += OnRotationChanged;

            SetRotation(_rotationProperty.Value);
        }

        public void ResetBindingContext(IObjectProvider objectProvider)
        {
            if (_rotationProperty == null)
                return;
            
            _rotationProperty.ValueChanged -= OnRotationChanged;
            objectProvider.ReturnProperty(_rotationProperty);
            _rotationProperty = null;
            
            SetRotation(0f);
        }

        private void OnRotationChanged(object sender, float newValue)
        {
            SetRotation(newValue);
        }

        private void SetRotation(float newValue)
        {
            style.rotate = new Rotate(newValue);
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _bindingImageAttribute = new()
                { name = "binding-rotation-path", defaultValue = string.Empty };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                var element = (BindableRotationElement)visualElement;
                element.BindingRotationPath = _bindingImageAttribute.GetValueFromBag(bag, context);
            }
        }

        public new class UxmlFactory : UxmlFactory<BindableRotationElement, UxmlTraits> { }
    }
}