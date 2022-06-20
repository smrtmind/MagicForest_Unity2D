using Scripts.Player;
using UnityEngine;

namespace Scripts.Objects
{
    public class ObstacleComponent : MonoBehaviour
    {
        private GameSession _session;
        private Collider2D _collider;

        private void Awake()
        {
            Destroy(gameObject, 10f);

            _session = FindObjectOfType<GameSession>();
            _collider = GetComponent<Collider2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                _collider.isTrigger = true;
                _session.GameIsOver();
            }
        }
    }
}
