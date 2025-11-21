using System;
using UnityEngine;

namespace MewtonGames.Audio.Providers
{
    public class ResourcesAudioProvider : IAudioProvider
    {
        public bool isInitialized { get; private set; }

        private readonly string _soundsPath;
        private readonly string _musicPath;

        public ResourcesAudioProvider(string soundsPath, string musicPath)
        {
            _soundsPath = soundsPath;
            _musicPath = musicPath;
        }

        public void Initialize(Action onComplete = null)
        {
            isInitialized = true;
            onComplete?.Invoke();
        }


        public void LoadSoundAudioClip(string soundId, Action<AudioClip> onComplete)
        {
            var path = $"{_soundsPath}/{soundId}";
            var clip = Resources.Load<AudioClip>(path);
            onComplete?.Invoke(clip);
        }

        public void LoadSoundAudioClip(string soundId, string audioMixerGroup, Action<AudioClip, string> onComplete)
        {
            var path = $"{_soundsPath}/{soundId}";
            var clip = Resources.Load<AudioClip>(path);
            onComplete?.Invoke(clip, audioMixerGroup);
        }
        

        public void LoadMusicAudioClip(string musicId, Action<AudioClip> onComplete)
        {
            var path = $"{_musicPath}/{musicId}";
            var clip = Resources.Load<AudioClip>(path);
            onComplete?.Invoke(clip);
        }
        
        public void LoadMusicAudioClip(string musicId, string audioMixerGroup, Action<AudioClip, string> onComplete)
        {
            var path = $"{_musicPath}/{musicId}";
            var clip = Resources.Load<AudioClip>(path);
            onComplete?.Invoke(clip, audioMixerGroup);
        }
    }
}