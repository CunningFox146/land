using System;
using System.Collections.Generic;

namespace Land.Localization
{
    public interface ILocalizationService
    {
        event Action LocaleChanged;
        string CurrentLocale { get; }
        public List<string> AllLocales { get; }
        void ChangeLocale(string localeName);
        string GetString(string key);
    }
}