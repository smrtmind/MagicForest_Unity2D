using UnityEngine;

namespace Scripts.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerController _player;

        private void Awake()
        {
            _player = GetComponent<PlayerController>();
        }

        private void Update()
        {
            _player.horizontalMovement = Input.GetAxisRaw("Horizontal");
            _player.jump = Input.GetButtonDown("Jump");
        }
    }
}
