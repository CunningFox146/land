using DG.Tweening;
using UnityMvvmToolkit.Core.Interfaces;

namespace Land.Utils
{
    public static class PropertyExtensions
    {
        public static Tween To(this IProperty<float> property, float endValue, float duration)
        {
            return DOTween.To(
                () => property.Value,
                val => property.Value = val, 
                endValue, 
                duration);
        }
    }
}