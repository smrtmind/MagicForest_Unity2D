using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class UIcontroller : MonoBehaviour
    {
        [SerializeField] private Image[] _buttonImage;
        [SerializeField] private Text[] _buttonText;

        private Color _defaultButtonColor;
        private Color _defaultButtonTextColor;

        private void Awake()
        {
            _defaultButtonColor = _buttonImage[0].color;
            _defaultButtonTextColor = _buttonText[0].color;
        }

        public void OnButtonPressed(int buttonIndex)
        {
            _buttonImage[buttonIndex].color = Color.green;
            _buttonText[buttonIndex].color = Color.white;
        }

        public void OnButtonReleased()
        {
            foreach (var buttonImage in _buttonImage)
                buttonImage.color = _defaultButtonColor;

            foreach (var buttonText in _buttonText)
                buttonText.color = _defaultButtonTextColor;
        }
    }
}
