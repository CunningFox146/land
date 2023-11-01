using DG.Tweening;
using UnityEngine.UIElements;

namespace Land.Utils
{
    public static class UIUtil
    {
        
        public static void Hide(this VisualElement visualElement)
        {
            visualElement.style.display = DisplayStyle.None;
            visualElement.style.visibility = Visibility.Hidden;
        }
        
        public static void Show(this VisualElement visualElement)
        {
            visualElement.style.display = DisplayStyle.Flex;
            visualElement.style.visibility = Visibility.Visible;
        }
        
        public static void ToggleDisplay(this VisualElement visualElement)
        {
            if (visualElement.style.display.value is DisplayStyle.Flex)
                visualElement.Hide();
            else
                visualElement.Show();

        }

        public static Tween DOFade(this VisualElement element, float target, float duration)
        {
            return DOTween.To(
                () => element.style.opacity.value,
                f => element.style.opacity = f,
                target,
                duration
            );
        }

        public static VisualElement GetDocumentRoot(this VisualElement element)
        {
            while (element.parent != null)
            {
                element = element.parent;
            }
            return element;
        }
    }
}