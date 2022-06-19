using Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private Text _coinsValue;
        [SerializeField] private Button _menuButton;

        private GameSession _session;
        private Color _pressedButtonColor;

        private void Awake()
        {
            _session = FindObjectOfType<GameSession>();

            _pressedButtonColor = _menuButton.GetComponent<Image>().color;
        }

        private void Update()
        {
            _coinsValue.text = $"{_session.Coins}";
        }

        public void OnButtonPressd()
        {
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
            SceneManager.LoadScene("MainMenu");
        }
    }
}
