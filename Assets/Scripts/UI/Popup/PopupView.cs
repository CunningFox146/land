using System;
using DG.Tweening;
using Land.UI.Main;
using Land.Utils;
using UnityEngine.UIElements;

namespace Land.UI.Popup
{
    public class PopupView : BaseView<PopupViewModel>
    {
        public override void Show()
        {
            base.Show();
            uiDocument.rootVisualElement.DOFade(1f, 0.1f).SetEase(Ease.OutSine);
        }

        public override void Hide()
        {
            base.Hide();
            uiDocument.rootVisualElement.style.opacity = 0f;
        }
    }
}