using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Land.Localization
{
    public class LocalizationService : ILocalizationService
    {
        public event Action LocaleChanged;
        private readonly IEnumerable<LocalizationData> _allLocales;
        private LocalizationData _lookup;
        public List<string> AllLocales => _allLocales.Select(l => l.localeName).ToList();

        public LocalizationService(IEnumerable<LocalizationData> allLocales)
        {
            _allLocales = allLocales;

            ChangeLocale(_allLocales.First());
        }

        public string CurrentLocale { get; private set; }

        public void ChangeLocale(string localeName)
        {
            ChangeLocale(_allLocales.FirstOrDefault(l => l.localeName == localeName));
        }
        
        private void ChangeLocale(LocalizationData locale)
        {
            _lookup = locale;
            CurrentLocale = locale.localeName;
            LocaleChanged?.Invoke();
        }

        public string GetString(string key)
        {
            if (_lookup.strings.TryGetValue(key, out var value))
                return value;
            
            Debug.LogWarning($"[{nameof(LocalizationService)}] No key {key} found in locale {CurrentLocale}");
            return string.Empty;

        }
    }
}