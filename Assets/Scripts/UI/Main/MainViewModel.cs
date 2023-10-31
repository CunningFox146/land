using Land.Localization;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Interfaces;

namespace Land.UI.Main
{
    public class MainViewModel : IBindingContext
    {
        private readonly ILocalizationService _localizationService;
        public IProperty<string> HeaderText { get; }
        public IProperty<string> BodyText { get; }
        public IProperty<bool> LanguagePickerVisible { get; }
        public IProperty<bool> IsEnVisible { get; }
        public IProperty<bool> IsGeVisible { get; }
        public ICommand LanguagePickCommand { get; }
        public ICommand<string> SetLanguageCommand { get; }
        
        public MainViewModel(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
                
            HeaderText = new Property<string>();
            BodyText = new Property<string>();
            LanguagePickerVisible = new Property<bool>();
            IsGeVisible = new Property<bool>();
            IsEnVisible = new Property<bool>();
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

        private void OnLocaleChanged()
        {
            UpdateLocalization();
        }


        private void UpdateLocalization()
        {
            HeaderText.Value = _localizationService.GetString("bonus");
            BodyText.Value = _localizationService.GetString("body");
            
            IsGeVisible.Value = _localizationService.CurrentLocale == "de";
            IsEnVisible.Value = _localizationService.CurrentLocale == "en";
        }
    }
}