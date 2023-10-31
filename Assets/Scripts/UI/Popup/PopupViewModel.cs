using System;
using Land.Localization;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Interfaces;

namespace Land.UI.Popup
{
    public class PopupViewModel : IBindingContext
    {
        private readonly ILocalizationService _localizationService;

        public IProperty<string> YourBonus { get; }
        public IProperty<string> Info { get; }
        public IProperty<string> Download { get; }

        public PopupViewModel(ILocalizationService localizationService)
        {
            _localizationService = localizationService;

            YourBonus = new Property<string>();
            Info = new Property<string>();
            Download = new Property<string>();
            
            _localizationService.LocaleChanged += OnLocaleChanged;
            UpdateLocalization();
        }
        private void OnLocaleChanged()
        {
            UpdateLocalization();
        }

        private void UpdateLocalization()
        {
            YourBonus.Value = _localizationService.GetString("your_bonus");
            Info.Value = _localizationService.GetString("your_bonus_body");
            Download.Value = _localizationService.GetString("download");
        }
    }
}