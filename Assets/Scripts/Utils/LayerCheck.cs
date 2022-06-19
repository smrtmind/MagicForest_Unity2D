using Scripts.Player;
using UnityEngine;

namespace Scripts.Utils
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] private string _tag;

        private PlayerController _player;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == _tag)
                _player.IsGrounded = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == _tag)
                _player.IsGrounded = false;
        }
    }
}
