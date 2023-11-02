using System;
using Land.Utils;
using UnityEngine.UIElements;

namespace Land.UI.Main
{
    public class MainView : BaseView<MainViewModel>
    {
        private void Start()
        {
            this.Delay(.25f, () =>
            {
                uiDocument.rootVisualElement.Q(className: "slide-right")?.RemoveFromClassList("slide-right");
                uiDocument.rootVisualElement.Q(className: "slide-left")?.RemoveFromClassList("slide-left");
                uiDocument.rootVisualElement.Q(className: "slide-up")?.RemoveFromClassList("slide-up");
            });
        }
    }
}