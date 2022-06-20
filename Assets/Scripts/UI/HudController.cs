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

        private void Awake()
        {
            _session = FindObjectOfType<GameSession>();
            _pressedButtonColor = _menuButton.GetComponent<Image>().color;

            _audio = FindObjectOfType<AudioComponent>();
            _audio.SetMusicVolume(0.2f);
            _audio.SetMusicTrack("level");
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
                {
                    _audio.StopMusic();
                    SceneManager.LoadScene("MainMenu");
                }
            }

            _coinsValue.text = $"{_session.Coins}";
        }

        public void OnButtonPressd()
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
            _loadingLayoutCanvasGroup.gameObject.SetActive(true);
            _returntoMenuPressed = true;
        }
    }
}
