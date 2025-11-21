using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MewtonGames.Audio.Providers;
using UnityEngine;

namespace MewtonGames.Audio
{
    public class AudioModule : MonoBehaviour, IAudioModule
    {
        public event Action soundsVolumeChanged;
        
        public bool isInitialized { get; private set; }
        public float musicVolume { get; private set; } = 1f;
        public float soundsVolume { get; private set; } = 1f;

        private readonly Dictionary<int, AudioSource> _musicTracks = new();
        private readonly List<AudioSource> _sounds = new();
        private IAudioProvider _audioProvider;
        private IAudioMixerGroupProvider _audioMixerGroupProvider;
        private int _maxSoundsCount = 5;

        
        public void Initialize(IAudioProvider audioProvider, IAudioMixerGroupProvider audioMixerGroupProvider, int maxSoundsCount = 5, Action onComplete = null)
        {
            _audioProvider = audioProvider;
            _audioMixerGroupProvider = audioMixerGroupProvider;
            _maxSoundsCount = maxSoundsCount;
            Initialize(onComplete);
        }

        public void Initialize(Action onComplete = null)
        {
            if (isInitialized)
            {
                onComplete?.Invoke();
                return;
            }

            gameObject.AddComponent<AudioListener>();
            _audioProvider ??= new ResourcesAudioProvider("Audio/Sounds", "Audio/Music");
            _audioProvider.Initialize(() =>
            {
                isInitialized = true;
                onComplete?.Invoke();
            });
        }


        public void SetMusicVolume(float value)
        {
            if (value > 1f)
            {
                value = 1f;
            }

            musicVolume = value;

            foreach (var keyValuePair in _musicTracks)
            {
                keyValuePair.Value.volume = musicVolume;
            }
        }

        public void SetSoundsVolume(float value)
        {
            if (value > 1f)
            {
                value = 1f;
            }

            soundsVolume = value;

            foreach (var audioSource in _sounds)
            {
                audioSource.volume = value;
            }
            
            soundsVolumeChanged?.Invoke();
        }

        public void SetSoundsMaxCount(int value)
        {
            _maxSoundsCount = value;
        }

        public void PlayMusic(string musicId, int trackId = 0, float fadeTime = 0.5f)
        {
            PlayMusicInternal(musicId, null, trackId, fadeTime);
        }
        
        public void PlayMusic(string musicId, string audioMixerGroupId, int trackId = 0, float fadeTime = 0.5f)
        {
            PlayMusicInternal(musicId, audioMixerGroupId, trackId, fadeTime);
        }

        public void PauseMusic(int trackId = 0, float fadeTime = 0.5f)
        {
            if (_musicTracks.TryGetValue(trackId, out var existAudioSource))
            {
                StartCoroutine(FadeOut(existAudioSource, fadeTime, AfterFadeEndAction.Pause));
            }
        }

        public void UnPauseMusic(int trackId = 0, float fadeTime = 0.5f)
        {
            if (_musicTracks.TryGetValue(trackId, out var existAudioSource))
            {
                StartCoroutine(FadeIn(existAudioSource, fadeTime, musicVolume, BeforeFadeStartAction.UnPause));
            }
        }

        public void PauseAllMusic(float fadeTime = 0.5f)
        {
            foreach (var keyValuePair in _musicTracks)
            {
                StartCoroutine(FadeOut(keyValuePair.Value, fadeTime, AfterFadeEndAction.Pause));
            }
        }

        public void UnPauseAllMusic(float fadeTime = 0.5f)
        {
            foreach (var keyValuePair in _musicTracks)
            {
                StartCoroutine(FadeIn(keyValuePair.Value, fadeTime, musicVolume, BeforeFadeStartAction.UnPause));
            }
        }

        public void StopMusic(int trackId = 0, float fadeTime = 0)
        {
            if (_musicTracks.TryGetValue(trackId, out var existAudioSource))
            {
                StartCoroutine(FadeOut(existAudioSource, fadeTime, AfterFadeEndAction.Stop));
            }
        }

        public void StopAllMusic(float fadeTime = 0)
        {
            foreach (var keyValuePair in _musicTracks)
            {
                StartCoroutine(FadeOut(keyValuePair.Value, fadeTime, AfterFadeEndAction.Stop));
            }
        }

        public void FadeInMusic(int trackId = 0, float time = 0.5f)
        {
            if (_musicTracks.TryGetValue(trackId, out var existAudioSource))
            {
                StartCoroutine(FadeIn(existAudioSource, time, musicVolume));
            }
        }

        public void FadeOutMusic(int trackId = 0, float time = 0.5f)
        {
            if (_musicTracks.TryGetValue(trackId, out var existAudioSource))
            {
                StartCoroutine(FadeOut(existAudioSource, time));
            }
        }

        public void FadeInAllMusic(float time = 0.5f)
        {
            foreach (var keyValuePair in _musicTracks)
            {
                StartCoroutine(FadeIn(keyValuePair.Value, time, musicVolume));
            }
        }

        public void FadeOutAllMusic(float time = 0.5f)
        {
            foreach (var keyValuePair in _musicTracks)
            {
                StartCoroutine(FadeOut(keyValuePair.Value, time));
            }
        }
        
        public void PlaySound(string soundId)
        {
            _audioProvider.LoadSoundAudioClip(soundId, PlaySoundInternal);
        }

        public void PlaySound(string soundId, string audioMixerGroup)
        {
            _audioProvider.LoadSoundAudioClip(soundId, audioMixerGroup, PlaySoundInternal);
        }

        public void PlaySound(AudioClip clip)
        {
            PlaySoundInternal(clip);
        }
        
        public void PlaySound(AudioClip clip, string audioMixerGroup)
        {
            PlaySoundInternal(clip, audioMixerGroup);
        }

        public void StopAllSounds()
        {
            foreach (var audioSource in _sounds)
            {
                audioSource.Stop();
                audioSource.clip = null;
            }
        }


        private IEnumerator ReplaceMusic(AudioSource source, AudioClip newClip, float fadeInTime = 0.5f, float fadeOutTime = 0.5f)
        {
            yield return FadeOut(source, fadeOutTime, AfterFadeEndAction.Stop);
            source.clip = newClip;
            yield return FadeIn(source, fadeInTime, musicVolume, BeforeFadeStartAction.Play);
        }

        private void PlayMusicInternal(string musicId, string audioMixerGroupId, int trackId = 0, float fadeTime = 0.5f)
        {
            _audioProvider.LoadMusicAudioClip(musicId, clip =>
            {
                if (_musicTracks.TryGetValue(trackId, out var existAudioSource))
                {
                    if (existAudioSource.clip == clip)
                    {
                        UnPauseMusic(trackId, fadeTime);
                        return;
                    }

                    StartCoroutine(ReplaceMusic(existAudioSource, clip, fadeTime, fadeTime));
                    return;
                }

                var newAudioSource = CreateNewAudioSource();
                newAudioSource.gameObject.name = "MusicTrack_" + trackId;
                newAudioSource.clip = clip;
                newAudioSource.loop = true;
                newAudioSource.volume = 0f;
                if (!string.IsNullOrEmpty(audioMixerGroupId))
                {
                    newAudioSource.outputAudioMixerGroup = _audioMixerGroupProvider.Get(audioMixerGroupId);
                }
                
                _musicTracks.Add(trackId, newAudioSource);
                StartCoroutine(FadeIn(newAudioSource, fadeTime, musicVolume, BeforeFadeStartAction.Play));
            });
        } 
        
        private void PlaySoundInternal(AudioClip clip)
        {
            PlaySoundInternal(clip, null);
        }

        private void PlaySoundInternal(AudioClip clip, string audioMixerGroup)
        {
            var freeAudioSource = _sounds.FirstOrDefault(s => !s.isPlaying || s.clip == null);
            if (freeAudioSource != null)
            {
                freeAudioSource.outputAudioMixerGroup = !string.IsNullOrEmpty(audioMixerGroup) 
                    ? _audioMixerGroupProvider.Get(audioMixerGroup) 
                    : null;
                
                freeAudioSource.clip = clip;
                freeAudioSource.Play();
                return;
            }

            if (_sounds.Count >= _maxSoundsCount)
            {
                return;
            }

            var newAudioSource = CreateNewAudioSource();
            newAudioSource.gameObject.name = "SoundTrack_" + _sounds.Count;
            newAudioSource.clip = clip;
            newAudioSource.volume = soundsVolume;
            newAudioSource.outputAudioMixerGroup = !string.IsNullOrEmpty(audioMixerGroup) 
                ? _audioMixerGroupProvider.Get(audioMixerGroup) 
                : null;
            newAudioSource.Play();
            _sounds.Add(newAudioSource);
        }

        private IEnumerator FadeIn(AudioSource source, float time = 0.5f, float targetVolume = 1f, BeforeFadeStartAction startAction = BeforeFadeStartAction.None)
        {
            switch (startAction)
            {
                case BeforeFadeStartAction.None:
                    break;

                case BeforeFadeStartAction.UnPause:
                    source.UnPause();
                    break;

                case BeforeFadeStartAction.Play:
                    source.Play();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(startAction), startAction, null);
            }

            var currentFadeTime = time;
            var startVolume = targetVolume;

            while (currentFadeTime > 0f)
            {
                currentFadeTime -= UnityEngine.Time.deltaTime;
                var currentVolume = (1 - currentFadeTime / time) * startVolume;

                source.volume = currentVolume;
                yield return null;
            }
        }

        private IEnumerator FadeOut(AudioSource source, float time = 0.5f, AfterFadeEndAction endAction = AfterFadeEndAction.None)
        {
            var currentFadeTime = time;
            var startVolume = source.volume;

            while (currentFadeTime > 0f)
            {
                currentFadeTime -= UnityEngine.Time.deltaTime;
                var currentVolume = currentFadeTime / time * startVolume;

                source.volume = currentVolume;
                yield return null;
            }

            switch (endAction)
            {
                case AfterFadeEndAction.None:
                    break;

                case AfterFadeEndAction.Pause:
                    source.Pause();
                    break;

                case AfterFadeEndAction.Stop:
                    source.Stop();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(endAction), endAction, null);
            } 
        }

        private AudioSource CreateNewAudioSource()
        {
            var newGameObject = new GameObject();
            newGameObject.transform.SetParent(transform);

            var audioSource = newGameObject.AddComponent<AudioSource>();
            return audioSource;
        }

        private enum BeforeFadeStartAction
        {
            None,
            UnPause,
            Play
        }

        private enum AfterFadeEndAction
        {
            None,
            Pause,
            Stop
        }
    }
}