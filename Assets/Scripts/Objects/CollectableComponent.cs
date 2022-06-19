using Scripts.Player;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Objects
{
    public class CollectableComponent : MonoBehaviour
    {
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private float _coinLifeSpan = 5f;

        private AudioComponent _audio;
        private GameSession _session;
        private Rigidbody2D _coin;
        private SpriteAnimation _spriteAnimation;

        private void Awake()
        {
            Destroy(gameObject, _coinLifeSpan);

            _audio = FindObjectOfType<AudioComponent>();
            _session = FindObjectOfType<GameSession>();
            _coin = GetComponent<Rigidbody2D>();
            _spriteAnimation = GetComponent<SpriteAnimation>();
        }

        private void Update()
        {
            if (_groundCheck.IsTouchingLayer)
            {
                _coin.bodyType = RigidbodyType2D.Static;
                _spriteAnimation.enabled = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                _audio.Play("coin");
                _session.AddCoin(1);

                Destroy(gameObject);
            }
        }
    }
}
