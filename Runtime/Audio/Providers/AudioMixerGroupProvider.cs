using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace MewtonGames.Audio.Providers
{
    [CreateAssetMenu(menuName = "MewtonGames/Audio/AudioMixerGroupProvider")]
    public class AudioMixerGroupProvider : ScriptableObject, IAudioMixerGroupProvider
    {
        [SerializeField] private List<AudioMixerGroupContainer> _audioMixerGroups;
        
        public AudioMixerGroup Get(string id)
        {
            return _audioMixerGroups.FirstOrDefault(c => c.id == id)?.group;
        }
    }

    [Serializable]
    public class AudioMixerGroupContainer
    {
        public string id;
        public AudioMixerGroup group;
    }
}