using UnityEngine;

namespace Scripts.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerController _player;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>();
        }

        private void Update()
        {
            //for PC build
            //*******************************************************************
            //_player.horizontalMovement = Input.GetAxisRaw("Horizontal");
            //_player.jump = Input.GetButtonDown("Jump");
            //*******************************************************************
        }

        //for mobile build
        //*******************************************************************
        public void LeftPressed(bool state) => _player.left = state;

        public void RightPressed(bool state) => _player.right = state;

        public void UpPressed() => _player.up = true;
        //*******************************************************************
    }
}
