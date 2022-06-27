using Scripts.UI;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private Animator _animator;

        //for PC build
        //*******************************************************************
        //public bool jump { get; set; }
        //public float horizontalMovement { get; set; }
        //*******************************************************************

        //for mobile build
        //*******************************************************************
        public bool leftPressed { get; set; }
        public bool rightPressed { get; set; }
        public bool jumpPressed { get; set; }
        //*******************************************************************

        private Rigidbody2D _playerBody;
        private Transform _transform;
        private GroundCheck _groundCheck;
        private GameSession _session;
        private AudioComponent _audio;
        private HudController _hud;
        private PlayerInput _input;

        private static readonly int Run = Animator.StringToHash("is-running");
        private static readonly int Jump = Animator.StringToHash("is-jumping");
        private static readonly int Fall = Animator.StringToHash("is-falling");

        private void Awake()
        {
            _playerBody = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
            _groundCheck = GetComponent<GroundCheck>();
            _session = FindObjectOfType<GameSession>();
            _audio = FindObjectOfType<AudioComponent>();
            _hud = FindObjectOfType<HudController>();
            _input = FindObjectOfType<PlayerInput>();
        }

        private void Update()
        {
            //for PC build
            //*******************************************************************
            //_playerBody.velocity = new Vector2(horizontalMovement * _speed, _playerBody.velocity.y);

            //if (jump && _groundCheck.IsTouchingLayer)
            //{
            //    _audio.PlaySfx("jump");
            //    _playerBody.velocity = new Vector2(_playerBody.velocity.x, _jumpForce);
            //}
            //*******************************************************************

            //for mobile build
            //*******************************************************************
            if (leftPressed)
                _playerBody.velocity = new Vector2(-1f * _speed, _playerBody.velocity.y);
            else if (rightPressed)
                _playerBody.velocity = new Vector2(1f * _speed, _playerBody.velocity.y);
            else
                _playerBody.velocity = new Vector2(0f, _playerBody.velocity.y);

            if (jumpPressed && _groundCheck.IsTouchingLayer)
            {
                _audio.PlaySfx("jump");
                _playerBody.velocity = new Vector2(_playerBody.velocity.x, _jumpForce);

                jumpPressed = false;
            }
            //*******************************************************************

            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            if (leftPressed)//horizontalMovement < 0f (for PC build)
            {
                _transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
                _animator.SetBool(Run, true);
            }
            else if (rightPressed)//horizontalMovement > 0f (for PC build)
            {
                _transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
                _animator.SetBool(Run, true);
            }
            else
            {
                _animator.SetBool(Run, false);
            }

            var positionY = _playerBody.velocity.y != 0f ? true : false;
            _animator.SetBool(Jump, positionY);

            if (_session.GameOver)
            {
                _animator.SetTrigger(Fall);
                StopPlayer();

                _hud.ReturnToMainMenu();
            }
        }

        public void StopPlayer()
        {
            //block player control buttons
            _input.enabled = false;
            _playerBody.velocity = new Vector2(0f, _playerBody.velocity.y);
        }
    }
}
