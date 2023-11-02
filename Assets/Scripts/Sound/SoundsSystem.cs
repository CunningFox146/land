using System;
using System.Collections.Generic;
using System.Linq;
using Land.Utils;
using UnityEngine;

namespace DefaultNamespace.Sound
{
    public class SoundsSystem : MonoBehaviour, ISoundsSystem
    {
        [SerializeField] private List<SoundInfo> _sounds;
        private readonly Dictionary<string, AudioSource> _audioSources = new();

        public void PlaySound(string soundName)
        {
            var info = _sounds.FirstOrDefault(s => s.Name == soundName);
            if (info is null)
                return;

            var obj = new GameObject(soundName);
            var audioSource = obj.AddComponent<AudioSource>();
            audioSource.loop = info.IsLoop;
            audioSource.clip = info.Audio;
            audioSource.Play();
            _audioSources.Add(soundName, audioSource);

            if (!info.IsLoop)
                this.Delay(audioSource.clip.length, () =>
                {
                    _audioSources.Remove(soundName);
                    Destroy(audioSource.gameObject);
                });
        }

        public void StopSound(string soundName)
        {
            if (_audioSources.TryGetValue(soundName, out var sound))
                Destroy(sound.gameObject);
            _audioSources.Remove(soundName);
        }

        public void SetVolume(string soundName, float volume)
        {
            if (_audioSources.TryGetValue(soundName, out var sound))
                sound.volume = volume;
        }
        
        public void SetPitch(string soundName, float pitch)
        {
            if (_audioSources.TryGetValue(soundName, out var sound))
                sound.pitch = pitch;
        }

        [Serializable]
        private class SoundInfo
        {
            [field: SerializeField] public string Name { get; private set; }
            [field: SerializeField] public AudioClip Audio { get; private set; }
            [field: SerializeField] public bool IsLoop { get; private set; }
        }
    }
}