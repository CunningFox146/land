using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Land.Localization;
using Land.Utils;
using UnityEngine;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Interfaces;
using Random = UnityEngine.Random;

namespace Land.UI.Main
{
    public class MainViewModel : IBindingContext
    {
        public event Action AnimDone;
        private readonly ILocalizationService _localizationService;
        private Sequence _anim;
        
        public IProperty<string> HeaderText { get; }
        public IProperty<string> BodyText { get; }
        public IProperty<string> TryHeadText { get; }
        public IProperty<string> TryBodyText { get; }
        public IProperty<string> NoHeadText { get; }
        public IProperty<string> NoBodyText { get; }
        public IProperty<string> SpinText { get; }
        public IProperty<bool> LanguagePickerVisible { get; }
        public IProperty<bool> IsEnVisible { get; }
        public IProperty<bool> IsGeVisible { get; }
        public IProperty<float> WheelRotation { get; }
        public ICommand LanguagePickCommand { get; }
        public ICommand<string> SetLanguageCommand { get; }
        public ICommand StartSpinCommand { get; }
        
        public MainViewModel(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
                
            HeaderText = new Property<string>();
            BodyText = new Property<string>();
            TryHeadText = new Property<string>();
            TryBodyText = new Property<string>();
            NoHeadText = new Property<string>();
            NoBodyText = new Property<string>();
            SpinText = new Property<string>();
            
            LanguagePickerVisible = new Property<bool>();
            IsGeVisible = new Property<bool>();
            IsEnVisible = new Property<bool>();
            WheelRotation = new Property<float>();
            LanguagePickCommand = new Command(() => LanguagePickerVisible.Value = !LanguagePickerVisible.Value);
            StartSpinCommand = new Command(StartSpin, () => _anim == null);
            SetLanguageCommand = new Command<string>(ChangeLocale);
            
            _localizationService.LocaleChanged += OnLocaleChanged;
            
            UpdateLocalization();
        }

        private void StartSpin()
        {
            var targetAngle = Random.value < 0.5f ? 180f : 0f;
            targetAngle += (22.5f + Random.value * 5f) * (Random.value < 0.5f ? -1f : 1f);
            _anim = DOTween.Sequence()
                    .Append(WheelRotation.To(-25f, 1f).SetEase(Ease.OutSine))
                    .Append(WheelRotation.To(360f * 4f + targetAngle, 8f).SetEase(Ease.OutQuint))
                ;
            // .OnComplete(() => AnimDone?.Invoke());
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
            TryHeadText.Value = _localizationService.GetString("try");
            TryBodyText.Value = _localizationService.GetString("again");
            NoHeadText.Value = _localizationService.GetString("no");
            NoBodyText.Value = _localizationService.GetString("profit");
            SpinText.Value = _localizationService.GetString("spin");
            
            IsGeVisible.Value = _localizationService.CurrentLocale == "de";
            IsEnVisible.Value = _localizationService.CurrentLocale == "en";
        }
    }
}