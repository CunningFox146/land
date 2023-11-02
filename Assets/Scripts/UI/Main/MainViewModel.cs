using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Sound;
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
        private readonly ISoundsSystem _soundsSystem;
        private Tween _idleAnim;
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
        
        public MainViewModel(ILocalizationService localizationService, ISoundsSystem soundsSystem)
        {
            _localizationService = localizationService;
            _soundsSystem = soundsSystem;

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

            _idleAnim = WheelRotation.To(-360f, 60f)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
            UpdateLocalization();
        }

        private void StartSpin()
        {
            _idleAnim?.Kill();
            _idleAnim = null;
            
            var targetAngle = Random.value < 0.5f ? 180f : 0f;
            targetAngle += (22f + Random.value * 5f) * (Random.value < 0.5f ? -1f : 1f);
            targetAngle += -360f * 5f;
            
            _soundsSystem.PlaySound("SlotStart");
            const float duration = 8f;
            _anim = DOTween.Sequence()
                .Append(WheelRotation.To( -360f, .9f).SetEase(Ease.InSine)
                    .OnUpdate(() =>
                    {
                        var progress = WheelRotation.Value / -360f;
                        _soundsSystem.SetVolume("SlotSpin", progress);
                        _soundsSystem.SetPitch("SlotSpin", Mathf.Lerp(0.7f, 1f, progress));
                    })
                    .OnStart(() =>
                    {
                        _soundsSystem.PlaySound("SlotSpin");
                    }))
                .Append(WheelRotation.To( targetAngle, duration).SetEase(Ease.OutQuint)
                    .OnUpdate(() =>
                    {
                        var progress = WheelRotation.Value / targetAngle;
                        _soundsSystem.SetVolume("SlotSpin", 1f - progress);
                        _soundsSystem.SetPitch("SlotSpin", Mathf.Lerp(1f, 0.85f, progress));
                    })
                )
                .OnComplete(() =>
                {
                    AnimDone?.Invoke();
                    _soundsSystem.PlaySound("Cheer");
                    _soundsSystem.PlaySound("Win");
                    _soundsSystem.StopSound("SlotSpin");
                    _anim = null;
                });
            StartSpinCommand.RaiseCanExecuteChanged();
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