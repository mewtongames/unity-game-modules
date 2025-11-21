using UnityEngine.Audio;

namespace MewtonGames.Audio.Providers
{
    public interface IAudioMixerGroupProvider
    {
        public AudioMixerGroup Get(string id);
    }
}