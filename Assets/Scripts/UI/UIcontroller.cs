using Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class UiController : MonoBehaviour
    {
        [SerializeField] private GameObject _networkError;
        [SerializeField] private Text _networkMessage;
        [SerializeField] private CanvasGroup _mainSettingsCanvasGroup;
        [SerializeField] private CanvasGroup _loadingLayoutCanvasGroup;
        [SerializeField] private Button[] _buttons;
        [SerializeField] private GameObject _sfxCheckbox;
        [SerializeField] private GameObject _musicCheckbox;
        [SerializeField] private float _menuSlideSpeed = 5f;
        [SerializeField] private float _fadeSpeed = 0.2f;

        private RectTransform _canvasRect;
        private GridLayoutGroup _grid;
        private AudioComponent _audio;
        private LoadAssetBundles _bundlesLoader;
        private Color _pressedButtonColor;
        private Vector3 _defaultMenuPosition;
        private float _canvasWidth;
        private float _canvasHeight;
        private bool _showMenu;
        private bool _showSettings;
        private bool _playIsPressed;
        private int _buttonIndex;
        private float _errorMessageDelay = 2f;
        private float _loadingDelay = 3f;

        private void Awake()
        {
            _audio = FindObjectOfType<AudioComponent>();
            _audio.SetMusicTrack("intro");
            _grid = FindObjectOfType<GridLayoutGroup>();
            _canvasRect = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
            _pressedButtonColor = _buttons[0].transform.Find("Text").GetComponent<Text>().color;
            _bundlesLoader = FindObjectOfType<LoadAssetBundles>();
        }

        private void Start()
        {
            _defaultMenuPosition = transform.position;
            _canvasWidth = _canvasRect.rect.width;
            _canvasHeight = _canvasRect.rect.height;

            _grid.cellSize = new Vector2(_canvasWidth, _canvasHeight);

            _sfxCheckbox.SetActive(_audio.SfxSource.volume > 0f ? true : false);
            _musicCheckbox.SetActive(_audio.MusicSource.volume > 0f ? true : false);
        }

        private void Update()
        {
            if (_networkError.activeSelf)
            {
                if (_errorMessageDelay > 0f)
                    _errorMessageDelay -= Time.deltaTime;
                else
                {
                    _networkError.SetActive(false);
                    _errorMessageDelay = 2f;
                }
            }

            if (_playIsPressed)
            {
                _mainSettingsCanvasGroup.alpha -= _fadeSpeed * Time.deltaTime;
                _loadingLayoutCanvasGroup.alpha += _fadeSpeed * Time.deltaTime;

                if (_mainSettingsCanvasGroup.alpha == 0)
                    _mainSettingsCanvasGroup.gameObject.SetActive(false);

                if (_loadingDelay > 0)
                    _loadingDelay -= Time.deltaTime;
                else
                {
                    if (_bundlesLoader.DownloadComplete)
                        SceneManager.LoadScene("EndlessLevel");
                    else
                        _loadingDelay += 1f;
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

        public void OnButtonPressed(int buttonIndex)
        {
            _buttonIndex = buttonIndex;

            _audio.PlaySfx("button");
            ChangeButtonColor(_pressedButtonColor, Color.white);
        }

        public void OnButtonReleased()
        {
            ChangeButtonColor(Color.white, _pressedButtonColor);
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
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                _networkMessage.text = "CHECK  INTERNET  CONNECTION";
                _networkError.SetActive(true);
            }
            else
            {
                _audio.StopMusic();
                _loadingLayoutCanvasGroup.gameObject.SetActive(true);

                _playIsPressed = true;
                _bundlesLoader.DownloadAssetBundles();
            }
        }

        public void ButtonsIsActive(bool state)
        {
            foreach (var button in _buttons)
                button.GetComponent<EventTrigger>().enabled = state;
        }

        public void ShowMenu() => _showMenu = true;

        public void ShowSettings() => _showSettings = true;

        public void OnExit() => Application.Quit();

        public void LoadingInterrupt()
        {
            _networkMessage.text = "SOMETHING  WENT  WRONG";
            _networkError.SetActive(true);

            _audio.PlayMusic();
            _loadingLayoutCanvasGroup.gameObject.SetActive(false);

            _playIsPressed = false;
        }

        private void ChangeButtonColor(Color buttonColor, Color buttonTextColor)
        {
            _buttons[_buttonIndex].GetComponent<Image>().color = buttonColor;
            _buttons[_buttonIndex].transform.Find("Text").GetComponent<Text>().color = buttonTextColor;
        }
    }
}
