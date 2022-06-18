using System;
using UnityEngine;

namespace Scripts.Utils
{
    public class AudioComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _mainSource;
        [SerializeField] private Sound[] _clips;

        //public AudioSource MainSource => _mainSource;

        private AudioSource _audio;
        private float _sfxVolume = 0.5f;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
        }

        public void Play(string name)
        {
            foreach (var clip in _clips)
            {
                if (clip.Name == name)
                {
                    _audio.volume = _sfxVolume;
                    _audio.PlayOneShot(clip.Clip);
                }
            }
        }

        public void SetSfxVolume(float volume) => _sfxVolume = volume;

        public void PauseMainSource() => _mainSource.Pause();

        public void PlayMainSource() => _mainSource.Play();

        //public void StopMainSource() => _mainSource.Stop();

        public void Stop() => _audio.Stop();
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




















//using System;
//using UnityEngine;

//namespace Scripts.Utils
//{
//    public class AudioComponent : MonoBehaviour
//    {
//        [SerializeField] private AudioSource _mainSource;
//        [SerializeField] private Sound[] _clips;

//        public AudioSource MainSource => _mainSource;

//        private AudioSource _audio;

//        private void Awake()
//        {
//            _audio = GetComponent<AudioSource>();
//        }

//        public void Play(string name, float volume = 0.2f)
//        {
//            foreach (var clip in _clips)
//            {
//                if (clip.Name == name)
//                {
//                    _audio.volume = volume;
//                    _audio.PlayOneShot(clip.Clip);
//                }
//            }
//        }

//        public void PauseMainSource() => _mainSource.Pause();

//        public void PlayMainSource() => _mainSource.Play();

//        public void StopMainSource() => _mainSource.Stop();

//        public void Stop() => _audio.Stop();
//    }

//    [Serializable]
//    public class Sound
//    {
//        [SerializeField] private string _name;
//        [SerializeField] private AudioClip _clip;

//        public string Name => _name;
//        public AudioClip Clip => _clip;
//    }
//}

