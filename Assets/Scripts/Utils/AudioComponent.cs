using System;
using UnityEngine;

namespace Scripts.Utils
{
    public class AudioComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private Sound[] _clips;

        private float _sfxVolume = 0.5f;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void PlaySfx(string name)
        {
            foreach (var clip in _clips)
            {
                if (clip.Name == name)
                {
                    _sfxSource.volume = _sfxVolume;
                    _sfxSource.PlayOneShot(clip.Clip);
                }
            }
        }

        public void SetSfxVolume(float volume) => _sfxVolume = volume;

        public void SetMusicVolume(float volume) => _musicSource.volume = volume;

        public void SetMusicTrack(string name)
        {
            foreach (var clip in _clips)
            {
                if (clip.Name == name)
                {
                    _musicSource.clip = clip.Clip;
                    _musicSource.Play();
                }
            }
        }

        public void PlayMusic() => _musicSource.Play();

        public void StopMusic() => _musicSource.Stop();
    }

    [Serializable]
    public class Sound
    {
        [SerializeField] private string _name;
        [SerializeField] private AudioClip _clip;

        public string Name => _name;
        public AudioClip Clip => _clip;
    }
}
