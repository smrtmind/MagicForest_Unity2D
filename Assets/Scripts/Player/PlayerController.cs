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
        public bool jump { get; set; }
        public float horizontalMovement { get; set; }
        //*******************************************************************

        //for mobile build
        //*******************************************************************
        public bool left { get; set; }
        public bool right { get; set; }
        public bool up { get; set; }
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
            if (left)
                _playerBody.velocity = new Vector2(-1f * _speed, _playerBody.velocity.y);
            else if (right)
                _playerBody.velocity = new Vector2(1f * _speed, _playerBody.velocity.y);
            else
                _playerBody.velocity = new Vector2(0f, _playerBody.velocity.y);

            if (up && _groundCheck.IsTouchingLayer)
            {
                _audio.PlaySfx("jump");
                _playerBody.velocity = new Vector2(_playerBody.velocity.x, _jumpForce);
                up = false;
            }
            //*******************************************************************

            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            if (left)//horizontalMovement < 0f
            {
                _transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
                _animator.SetBool(Run, true);
            }

            else if (right)//horizontalMovement > 0f
            {
                _transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
                _animator.SetBool(Run, true);
            }
            else
            {
                _animator.SetBool(Run, false);
            }

            if (_playerBody.velocity.y != 0f)
                _animator.SetBool(Jump, true);
            else
                _animator.SetBool(Jump, false);

            if (_session.GameOver)
            {
                _animator.SetTrigger(Fall);
                StopPlayer();

                _hud.ReturnToMainMenu();
            }
        }

        public void StopPlayer()
        {
            _input.enabled = false;
            _playerBody.velocity = new Vector2(0f, _playerBody.velocity.y);
        }
    }
}
