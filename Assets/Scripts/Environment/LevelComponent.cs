using Scripts.Player;
using UnityEngine;

namespace Scripts.Environment
{
    public class LevelComponent : MonoBehaviour
    {
        [SerializeField] private GameObject[] _sections;
        [SerializeField] private float _sectionLong = 21f;
        [SerializeField] private int _spawnSectionsOnStartEachSide;
        [SerializeField] private bool _spawnRightSide;
        [SerializeField] private bool _spawnLeftSide;

        [Space(20f)]
        [SerializeField] private GameObject _testSection;

        private float _xPositionRight;
        private float _xPositionLeft;
        private PlayerController _playerController;

        private void Awake()
        {
            Instantiate(_sections[GetRandomIndex()], new Vector3(0, 0, 0), Quaternion.identity);

            _xPositionRight += _sectionLong;
            _xPositionLeft -= _sectionLong;

            _testSection.SetActive(false);

            for (int i = 0; i < _spawnSectionsOnStartEachSide; i++)
                SpawnSection();
        }

        public void SpawnSection()
        {
            if (_spawnRightSide)
            {
                Instantiate(_sections[GetRandomIndex()], new Vector3(_xPositionRight, 0, 0), Quaternion.identity);
                _xPositionRight += _sectionLong;
            }
            if (_spawnLeftSide)
            {
                Instantiate(_sections[GetRandomIndex()], new Vector3(_xPositionLeft, 0, 0), Quaternion.identity);
                _xPositionLeft -= _sectionLong;
            }
        }

        private int GetRandomIndex() => Random.Range(0, _sections.Length);
    }
}
