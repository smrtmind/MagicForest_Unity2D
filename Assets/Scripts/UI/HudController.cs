using Scripts.Player;
using Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private Text _coinsValue;
        [SerializeField] private Button _menuButton;
        [SerializeField] private CanvasGroup _loadingLayoutCanvasGroup;
        [SerializeField] private float _fadeSpeed = 0.2f;
        [SerializeField] private float _loadingDelay = 3f;

        private GameSession _session;
        private Color _pressedButtonColor;
        private bool _returntoMenuPressed;
        private bool _levelLoaded = true;
        private AudioComponent _audio;
        private PlayerController _player;

        private void Awake()
        {
            _session = FindObjectOfType<GameSession>();
            _pressedButtonColor = _menuButton.GetComponent<Image>().color;
            _player = FindObjectOfType<PlayerController>();
            _audio = FindObjectOfType<AudioComponent>();
        }

        private void Start()
        {
            _audio.SetMusicTrack("level");

            if (_audio.MusicSource.volume != 0f)
                _audio.SetMusicVolume(0.2f);
        }

        private void Update()
        {
            if (_levelLoaded)
            {
                _loadingLayoutCanvasGroup.alpha -= _fadeSpeed * Time.deltaTime;

                if (_loadingLayoutCanvasGroup.alpha == 0)
                {
                    _loadingLayoutCanvasGroup.gameObject.SetActive(false);
                    _levelLoaded = false;
                }
            }

            if (_returntoMenuPressed)
            {
                _loadingLayoutCanvasGroup.alpha += _fadeSpeed * Time.deltaTime;

                if (_loadingDelay > 0)
                    _loadingDelay -= Time.deltaTime;
                else
                    SceneManager.LoadScene("MainMenu");
            }

            _coinsValue.text = $"{_session.Coins}";
        }

        public void OnButtonPressed()
        {
            _audio.PlaySfx("button");

            _menuButton.GetComponent<Image>().color = Color.white;
            _menuButton.transform.Find("Text").GetComponent<Text>().color = _pressedButtonColor;
        }

        public void OnButtonReleased()
        {
            _menuButton.GetComponent<Image>().color = _pressedButtonColor;
            _menuButton.transform.Find("Text").GetComponent<Text>().color = Color.white;
        }

        public void OnMenu()
        {
            _audio.StopMusic();
            ReturnToMainMenu();

            var spawners = FindObjectsOfType<ObjectSpawner>();
            foreach (var spawner in spawners)
                spawner.gameObject.SetActive(false);

            _player.StopPlayer();
        }

        public void ReturnToMainMenu()
        {
            _loadingLayoutCanvasGroup.gameObject.SetActive(true);
            _returntoMenuPressed = true;
        }
    }
}
