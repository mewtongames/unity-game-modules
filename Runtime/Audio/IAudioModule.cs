using System;
using MewtonGames.Audio.Providers;
using MewtonGames.Common;
using UnityEngine;

namespace MewtonGames.Audio
{
    public interface IAudioModule : IInitializable
    {
        public event Action soundsVolumeChanged;
        
        public float musicVolume { get; }
        public float soundsVolume { get; }

        public void Initialize(IAudioProvider provider, IAudioMixerGroupProvider audioMixerGroupProvider, int maxSoundsCount = 5, Action onComplete = null);

        public void SetMusicVolume(float value);
        public void SetSoundsVolume(float value);

        public void SetSoundsMaxCount(int value);

        public void PlayMusic(string musicId, int trackId = 0, float fadeTime = 0.5f);
        public void PlayMusic(string musicId, string audioMixerGroup, int trackId = 0, float fadeTime = 0.5f);
        public void PauseMusic(int trackId = 0, float fadeTime = 0.5f);
        public void PauseAllMusic(float fadeTime = 0.5f);
        public void UnPauseMusic(int trackId = 0, float fadeTime = 0.5f);
        public void UnPauseAllMusic(float fadeTime = 0.5f);
        public void StopMusic(int trackId = 0, float fadeTime = 0f);
        public void StopAllMusic(float fadeTime = 0f);

        public void FadeInMusic(int trackId = 0, float time = 0.5f);
        public void FadeOutMusic(int trackId = 0, float time = 0.5f);
        public void FadeInAllMusic(float time = 0.5f);
        public void FadeOutAllMusic(float time = 0.5f);

        public void PlaySound(string soundId);
        public void PlaySound(string soundId, string audioMixerGroup);
        public void PlaySound(AudioClip clip, string audioMixerGroup);
        public void StopAllSounds();
    }
}