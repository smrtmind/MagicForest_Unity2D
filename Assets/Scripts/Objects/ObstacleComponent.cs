using Scripts.Player;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Objects
{
    public class ObstacleComponent : MonoBehaviour
    {
        private GameSession _session;
        private Collider2D _collider;
        private AudioComponent _audio;

        private void Awake()
        {
            Destroy(gameObject, 10f);

            _session = FindObjectOfType<GameSession>();
            _collider = GetComponent<Collider2D>();
            _audio = FindObjectOfType<AudioComponent>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                _audio.PlaySfx("hit");
                _audio.PlaySfx("hurt");
                _collider.isTrigger = true;
                _session.GameIsOver();
            }
        }
    }
}
