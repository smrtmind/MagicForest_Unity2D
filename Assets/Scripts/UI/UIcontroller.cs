using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class UiController : MonoBehaviour
    {
        //[SerializeField] private Color _buttonsAndTextColor;
        [SerializeField] private Button[] _buttons;

        //[SerializeField] private Image[] _buttonImage;
        //[SerializeField] private Text[] _buttonText;
        [SerializeField] private float _menuSlideSpeed = 1f;

        private Color _defaultButtonColor;
        private CanvasScaler _canvas;
        private float _canvasWidth;
        private float _canvasHeight;
        private GridLayoutGroup _grid;
        private Vector3 _defaultMenuPosition;

        private bool _showMenu;
        private bool _showSettings;

        private void Awake()
        {
            _defaultMenuPosition = transform.position;
            _grid = FindObjectOfType<GridLayoutGroup>();
            _canvas = FindObjectOfType<Canvas>().GetComponent<CanvasScaler>();
            _canvasWidth = _canvas.referenceResolution.x;
            _canvasHeight = _canvas.referenceResolution.y;

            _defaultButtonColor = _buttons[0].transform.Find("Text").GetComponent<Text>().color;
        }

        private void Start()
        {
            _grid.cellSize = new Vector2(_canvasWidth, _canvasHeight);
        }

        public void OnButtonPressed(int buttonIndex)
        {
            _buttons[buttonIndex].GetComponent<Image>().color = _defaultButtonColor;
            _buttons[buttonIndex].transform.Find("Text").GetComponent<Text>().color = Color.white;

            //_buttonImage[buttonIndex].color = Color.green;
            //_buttonText[buttonIndex].color = Color.white;
        }

        public void OnButtonReleased()
        {
            foreach (var button in _buttons)
            {
                button.GetComponent<Image>().color = Color.white;
                button.transform.Find("Text").GetComponent<Text>().color = _defaultButtonColor;
            }
            //    buttonImage.color = _defaultButtonColor;

            //foreach (var buttonText in _buttonText)
            //    buttonText.color = _defaultButtonTextColor;
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
