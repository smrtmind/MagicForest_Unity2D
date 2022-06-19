using Scripts.Player;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Environment
{
    public class LevelComponent : MonoBehaviour
    {
        [SerializeField] private GameObject[] _sections;
        [SerializeField] private float _sectionLong = 21f;

        [Space]
        [SerializeField] private int _spawnSectionsOnStart;

        private float _xPosition;
        private PlayerController _playerController;

        private void Awake()
        {
            _xPosition += _sectionLong;

            for (int i = 0; i < _spawnSectionsOnStart; i++)
                SpawnSection();
        }

        public void SpawnSection()
        {
            var randomIndex = Random.Range(0, _sections.Length);

            Instantiate(_sections[randomIndex], new Vector3(_xPosition, 0, 0), Quaternion.identity);
            _xPosition += _sectionLong;
        }
    }
}
