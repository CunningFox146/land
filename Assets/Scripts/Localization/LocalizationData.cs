using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Land.Localization
{
    [CreateAssetMenu(menuName = "Localization/Data")]
    public class LocalizationData : ScriptableObject
    {
        public string localeName;

        [SerializedDictionary("Key", "Translation")]
        public SerializedDictionary<string, string> strings;
    }
}