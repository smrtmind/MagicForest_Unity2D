using UnityEngine;

namespace Scripts.Utils
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] _objects;
        [SerializeField] private Cooldown _spawnCooldown;
        [SerializeField] private bool _startSpawn;

        public Cooldown SpawnCooldown => _spawnCooldown;
        public bool StartSpawn
        {
            get => _startSpawn;
            set => _startSpawn = value;
        }

        private Bounds _screenBounds;

        private void Awake()
        {
            _spawnCooldown.Reset();
        }

        private void Update()
        {
            if (_startSpawn)
            {
                if (_spawnCooldown.IsReady)
                {
                    SpawnNewObject();
                    _spawnCooldown.Reset();
                }
            }
        }

        private void SpawnNewObject()
        {
            int objectIndex;

            if (_objects.Length > 1)
                objectIndex = Random.Range(0, _objects.Length - 1);
            else
                objectIndex = 0;

            _screenBounds = FindObjectOfType<ScreenBounds>().borderOfBounds;

            var yPosition = _screenBounds.min.y;
            var xPosition = Random.Range(_screenBounds.min.x, _screenBounds.max.x);

            var randomSpawnPosition = new Vector3(xPosition, yPosition);

            Instantiate(_objects[objectIndex], randomSpawnPosition, Quaternion.identity, transform);
        }
    }
}
