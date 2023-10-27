using UnityEngine;
using UnityEngine.UIElements;

namespace Land.Views
{
    public abstract class View : ScriptableObject
    {
        [field: SerializeField] public VisualTreeAsset PortraitView { get; private set; }
        [field: SerializeField] public VisualTreeAsset LandscapeView { get; private set; }
    }
}