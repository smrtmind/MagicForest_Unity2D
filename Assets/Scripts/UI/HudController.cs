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

        private PlayerController _player;
        private AudioComponent _audio;
        private GameSession _session;
        private Image _button;
        private Text _buttonText;
        private ObjectSpawner[] _spawners;
        private Color _pressedButtonColor;
        private bool _returntoMenuPressed;
        private bool _levelLoaded = true;

        private void Awake()
        {
            _session = FindObjectOfType<GameSession>();
            _pressedButtonColor = _menuButton.GetComponent<Image>().color;
            _player = FindObjectOfType<PlayerController>();
            _audio = FindObjectOfType<AudioComponent>();
            _button = _menuButton.GetComponent<Image>();
            _buttonText = _menuButton.transform.Find("Text").GetComponent<Text>();
            _spawners = FindObjectsOfType<ObjectSpawner>();
        }

        private void Start()
        {
            _audio.SetMusicTrack("level");

            //if player didn't turn off music in the main menu
            if (_audio.MusicSource.volume != 0f)
                _audio.SetMusicVolume(0.2f);
        }

        private void Update()
        {
            //loading fade-out effect
            if (_levelLoaded)
            {
                _loadingLayoutCanvasGroup.alpha -= _fadeSpeed * Time.deltaTime;

                if (_loadingLayoutCanvasGroup.alpha == 0)
                {
                    _loadingLayoutCanvasGroup.gameObject.SetActive(false);
                    _levelLoaded = false;
                }
            }

            //loading fade-in effect
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

            _button.color = Color.white;
            _buttonText.color = _pressedButtonColor;
        }

        public void OnButtonReleased()
        {
            _button.color = _pressedButtonColor;
            _buttonText.color = Color.white;
        }

        public void OnMenu()
        {
            _audio.StopMusic();
            ReturnToMainMenu();

            //prevent further spawning of items, after player press return to menu
            foreach (var spawner in _spawners)
                spawner.gameObject.SetActive(false);

            //freeze player
            _player.StopPlayer();
        }

        public void ReturnToMainMenu()
        {
            //turn on loading layout
            _loadingLayoutCanvasGroup.gameObject.SetActive(true);
            _returntoMenuPressed = true;
        }
    }
}
