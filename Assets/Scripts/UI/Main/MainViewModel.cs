using Land.Localization;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Interfaces;

namespace Land.UI.Main
{
    public class MainViewModel : IBindingContext
    {
        private readonly ILocalizationService _localizationService;
        public IProperty<string> Header { get; }
        public IProperty<bool> LanguagePickerVisible { get; }
        public ICommand LanguagePickCommand { get; }
        public ICommand<string> SetLanguageCommand { get; }
        
        public MainViewModel(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
                
            Header = new Property<string>();
            LanguagePickerVisible = new Property<bool>();
            LanguagePickCommand = new Command(() => LanguagePickerVisible.Value = !LanguagePickerVisible.Value);
            SetLanguageCommand = new Command<string>(ChangeLocale);
            
            _localizationService.LocaleChanged += OnLocaleChanged;
            
            UpdateLocalization();
        }

        private void ChangeLocale(string localeName)
        {
            _localizationService.ChangeLocale(localeName);
            LanguagePickerVisible.Value = false;
        }

        private void OnLocaleChanged(object sender, string locale)
        {
            _localizationService.ChangeLocale(locale);
        }

        private void OnLocaleChanged()
        {
            UpdateLocalization();
        }

        private void UpdateLocalization()
        {
            Header.Value = _localizationService.GetString("test");
        }
    }
}