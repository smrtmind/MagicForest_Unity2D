using Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class UiController : MonoBehaviour
    {
        [SerializeField] private GameObject _mainSettingLayout;
        [SerializeField] private GameObject _loadingLayout;
        [SerializeField] private GameObject _levelLayout;
        [SerializeField] private CanvasGroup _mainSettingsCanvasGroup;
        [SerializeField] private CanvasGroup _loadingLayoutCanvasGroup;
        [SerializeField] private Button[] _buttons;
        [SerializeField] private float _menuSlideSpeed = 5f;
        [SerializeField] private float _fadeSpeed = 0.2f;

        private Color _pressedButtonColor;
        private CanvasScaler _canvasScaler;
        private float _canvasWidth;
        private float _canvasHeight;
        private GridLayoutGroup _grid;
        private Vector3 _defaultMenuPosition;
        private AudioComponent _audio;

        private bool _soundIsActive = true;
        private bool _musicIsActive = true;

        private bool _showMenu;
        private bool _showSettings;
        private bool _playIsPressed;
        [SerializeField] private float _loadingDelay = 1f;

        private void Awake()
        {
            _audio = FindObjectOfType<AudioComponent>();
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
        }

        public void OnButtonPressed(int buttonIndex)
        {
            _audio.Play("button");

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
            _audio.Play("button");

            if (_musicIsActive)
            {
                _musicIsActive = false;
                _audio.PauseMainSource();
            }
            else
            {
                _musicIsActive = true;
                _audio.PlayMainSource();
            }
        }

        public void OnSoundPressed()
        {
            if (_soundIsActive)
            {
                _soundIsActive = false;
                _audio.SetSfxVolume(0f);
            }
            else
            {
                _soundIsActive = true;
                _audio.SetSfxVolume(0.5f);
                _audio.Play("button");
            }
        }

        public void OnPlayPressed()
        {
            _loadingLayout.SetActive(true);
            _playIsPressed = true;
        }

        public void ButtonsIsActive(bool state)
        {
            foreach (var button in _buttons)
                button.GetComponent<EventTrigger>().enabled = state;
        }

        public void ShowMenu() => _showMenu = true;

        public void ShowSettings() => _showSettings = true;

        private void Update()
        {
            if (_playIsPressed)
            {
                _mainSettingsCanvasGroup.alpha -= _fadeSpeed * Time.deltaTime;
                _loadingLayoutCanvasGroup.alpha += _fadeSpeed * Time.deltaTime;

                if (_mainSettingsCanvasGroup.alpha == 0)
                {
                    _mainSettingLayout.SetActive(false);
                }

                if (_loadingDelay > 0)
                {
                    _loadingDelay -= Time.deltaTime;
                }
                else
                {
                    SceneManager.LoadScene("LevelScene");
                }
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
