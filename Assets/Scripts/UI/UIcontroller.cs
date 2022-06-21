using Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class UiController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _mainSettingsCanvasGroup;
        [SerializeField] private CanvasGroup _loadingLayoutCanvasGroup;
        [SerializeField] private Button[] _buttons;
        [SerializeField] private GameObject _sfxCheckbox;
        [SerializeField] private GameObject _musicCheckbox;
        [SerializeField] private float _menuSlideSpeed = 5f;
        [SerializeField] private float _fadeSpeed = 0.2f;

        private Color _pressedButtonColor;
        private CanvasScaler _canvasScaler;
        private float _canvasWidth;
        private float _canvasHeight;
        private GridLayoutGroup _grid;
        private Vector3 _defaultMenuPosition;
        private AudioComponent _audio;
        private bool _showMenu;
        private bool _showSettings;
        private bool _playIsPressed;
        private float _loadingDelay = 3f;

        private void Awake()
        {
            _audio = FindObjectOfType<AudioComponent>();
            _audio.SetMusicTrack("intro");

            _defaultMenuPosition = transform.position;
            _grid = FindObjectOfType<GridLayoutGroup>();
            _canvasScaler = FindObjectOfType<Canvas>().GetComponent<CanvasScaler>();
            _canvasWidth = _canvasScaler.referenceResolution.x;
            _canvasHeight = _canvasScaler.referenceResolution.y;

            _pressedButtonColor = _buttons[0].transform.Find("Text").GetComponent<Text>().color;
        }

        private void Start()
        {
            _grid.cellSize = new Vector2(_canvasWidth, _canvasHeight);

            if (_audio.SfxSource.volume > 0f)
                _sfxCheckbox.SetActive(true);
            else
                _sfxCheckbox.SetActive(false);

            if (_audio.MusicSource.volume > 0f)
                _musicCheckbox.SetActive(true);
            else
                _musicCheckbox.SetActive(false);
        }

        public void OnButtonPressed(int buttonIndex)
        {
            _audio.PlaySfx("button");

            _buttons[buttonIndex].GetComponent<Image>().color = _pressedButtonColor;
            _buttons[buttonIndex].transform.Find("Text").GetComponent<Text>().color = Color.white;
        }

        public void OnButtonReleased()
        {
            foreach (var button in _buttons)
            {
                button.GetComponent<Image>().color = Color.white;
                button.transform.Find("Text").GetComponent<Text>().color = _pressedButtonColor;
            }
        }

        public void OnMusicPressed()
        {
            _audio.PlaySfx("button");

            if (_audio.MusicSource.volume > 0f)
            {
                _audio.SetMusicVolume(0f);
                _musicCheckbox.SetActive(false);
            }
            else
            {
                _audio.SetMusicVolume(0.2f);
                _musicCheckbox.SetActive(true);
            }
        }

        public void OnSoundPressed()
        {
            if (_audio.SfxSource.volume > 0f)
            {
                _audio.SetSfxVolume(0f);
                _sfxCheckbox.SetActive(false);
            }
            else
            {
                _audio.SetSfxVolume(0.5f);
                _audio.PlaySfx("button");
                _sfxCheckbox.SetActive(true);
            }
        }

        public void OnPlayPressed()
        {
            _audio.StopMusic();

            _loadingLayoutCanvasGroup.gameObject.SetActive(true);
            _playIsPressed = true;
        }

        public void ButtonsIsActive(bool state)
        {
            foreach (var button in _buttons)
                button.GetComponent<EventTrigger>().enabled = state;
        }

        public void ShowMenu() => _showMenu = true;

        public void ShowSettings() => _showSettings = true;

        public void OnExit() => Application.Quit();

        private void Update()
        {
            if (_playIsPressed)
            {
                _mainSettingsCanvasGroup.alpha -= _fadeSpeed * Time.deltaTime;
                _loadingLayoutCanvasGroup.alpha += _fadeSpeed * Time.deltaTime;

                if (_mainSettingsCanvasGroup.alpha == 0)
                    _mainSettingsCanvasGroup.gameObject.SetActive(false);

                if (_loadingDelay > 0)
                    _loadingDelay -= Time.deltaTime;
                else
                    SceneManager.LoadScene("EndlessLevel");
            }

            if (_showSettings)
            {
                if (gameObject.transform.position.x <= -_canvasWidth)
                {
                    ButtonsIsActive(true);
                    _showSettings = false;
                    return;
                }

                gameObject.transform.position = new Vector3(transform.position.x - _menuSlideSpeed, transform.position.y);
            }
            if (_showMenu)
            {
                if (gameObject.transform.position == _defaultMenuPosition)
                {
                    ButtonsIsActive(true);
                    _showMenu = false;
                    return;
                }

                gameObject.transform.position = new Vector3(transform.position.x + _menuSlideSpeed, transform.position.y);
            }
        }
    }
}
