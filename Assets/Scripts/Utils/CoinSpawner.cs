using UnityEngine;

namespace Scripts.Utils
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _coin;
        [SerializeField] private Cooldown _spawnCooldown;

        public Cooldown SpawnCooldown => _spawnCooldown;

        private Bounds _screenBounds;

        private void Update()
        {
            if (_spawnCooldown.IsReady)
            {
                SpawnNewCoin();
                _spawnCooldown.Reset();
            }
        }

        private void SpawnNewCoin()
        {
            _screenBounds = FindObjectOfType<ScreenBounds>().borderOfBounds;

            var yPosition = _screenBounds.min.y;
            var xPosition = Random.Range(_screenBounds.min.x, _screenBounds.max.x);

            var randomSpawnPosition = new Vector3(xPosition, yPosition);

            Instantiate(_coin, randomSpawnPosition, Quaternion.identity, transform);
        }
    }
}
