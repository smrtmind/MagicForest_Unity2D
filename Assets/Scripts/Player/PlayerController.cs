using UnityEngine;

namespace Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _jumpForce = 5f;

        public bool jump { get; set; }
        public float horizontalMovement { get; set; }

        private Rigidbody2D _playerBody;
        private Animator _animator;
        [SerializeField] private bool _isGrounded;
        private Transform _transform;

        public bool IsGrounded
        {
            get => _isGrounded;
            set => _isGrounded = value;
        }

        private static readonly int Run = Animator.StringToHash("is-running");
        private static readonly int Jump = Animator.StringToHash("is-jumping");
        private static readonly int Fall = Animator.StringToHash("is-falling");

        private void Awake()
        {
            _playerBody = GetComponent<Rigidbody2D>();
            _animator = FindObjectOfType<Animator>();
            _transform = GetComponent<Transform>();
        }

        private void Update()
        {
            _playerBody.velocity = new Vector2(horizontalMovement * _speed, _playerBody.velocity.y);
            if (horizontalMovement < 0f)
            {
                _transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
                _animator.SetBool(Run, true);
            }

            else if (horizontalMovement > 0f)
            {
                _transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
                _animator.SetBool(Run, true);
            }
            else
            {
                _animator.SetBool(Run, false);
            }

            if (jump && _isGrounded)
            {
                _playerBody.velocity = new Vector2(_playerBody.velocity.x, _jumpForce);
                _animator.SetBool(Jump, true);
            }

            //if (_isGrounded)
            //    _animator.SetBool(Jump, false);
        }
    }
}
