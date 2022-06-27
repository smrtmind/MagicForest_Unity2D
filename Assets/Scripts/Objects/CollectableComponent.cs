using Scripts.Player;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Objects
{
    public class CollectableComponent : MonoBehaviour
    {
        [SerializeField] private float _coinLifeSpan = 5f;

        private AudioComponent _audio;
        private GameSession _session;
        private Rigidbody2D _coin;
        private SpriteAnimation _spriteAnimation;
        private GroundCheck _groundCheck;

        private void Awake()
        {
            //assign the life cycle of an object on start
            Destroy(gameObject, _coinLifeSpan);

            _audio = FindObjectOfType<AudioComponent>();
            _session = FindObjectOfType<GameSession>();
            _coin = GetComponent<Rigidbody2D>();
            _spriteAnimation = GetComponent<SpriteAnimation>();
            _groundCheck = GetComponent<GroundCheck>();
        }

        private void Update()
        {
            if (_groundCheck.IsTouchingLayer)
            {
                //prevent coin of touching the ground
                _coin.bodyType = RigidbodyType2D.Static;
                _spriteAnimation.enabled = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                _audio.PlaySfx("coin");
                _session.AddCoin(1);

                Destroy(gameObject);
            }
        }
    }
}
