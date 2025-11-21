using System;
using MewtonGames.Common;
using UnityEngine;

namespace MewtonGames.Audio.Providers
{
    public interface IAudioProvider : IInitializable
    {
        public void LoadMusicAudioClip(string musicId, Action<AudioClip> onComplete);
        public void LoadMusicAudioClip(string musicId, string audioMixerGroup, Action<AudioClip, string> onComplete);
        public void LoadSoundAudioClip(string soundId, Action<AudioClip> onComplete);
        public void LoadSoundAudioClip(string soundId, string audioMixerGroup, Action<AudioClip, string> onComplete);
    }
}