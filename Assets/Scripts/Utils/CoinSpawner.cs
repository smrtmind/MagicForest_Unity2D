using UnityEngine;

namespace Scripts.Utils
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _coin;
        [SerializeField] private int _coinsOnStart;
        [SerializeField] private Cooldown _spawnCooldown;

        public Cooldown SpawnCooldown => _spawnCooldown;

        private Bounds _screenBounds;

        private void Start()
        {
            _screenBounds = FindObjectOfType<ScreenBounds>().borderOfBounds;

            SpawnCoins(_coinsOnStart);
            _spawnCooldown.Reset();
        }

        private void Update()
        {
            if (_spawnCooldown.IsReady)
            {
                SpawnNewCoin();
                _spawnCooldown.Reset();
            }
        }

        private void SpawnCoins(int count)
        {
            for (int i = 0; i < count; i++)
                SpawnNewCoin();
        }

        private void SpawnNewCoin()
        {
            var yPosition = _screenBounds.min.y;
            var xPosition = Random.Range(_screenBounds.min.x, _screenBounds.max.x);

            var randomSpawnPosition = new Vector3(xPosition, yPosition);

            Instantiate(_coin, randomSpawnPosition, Quaternion.identity, transform);
        }
    }
}
