using Scripts.Player;
using System.Collections;
using UnityEngine;

namespace Scripts.Environment
{
    public class LevelComponent : MonoBehaviour
    {
        [SerializeField] private GameObject[] _sections;
        [SerializeField] private float _sectionLong = 21f;
        [SerializeField] private float _spawnDelay = 3f;

        [Space(20f)]
        [SerializeField] private GameObject _testSection;

        private float _xPositionRight;
        private float _xPositionLeft;
        private PlayerController _player;
        private bool _creatingSection = false;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>();

            //spawn first section on start in the middle of the scene
            Instantiate(_sections[GetRandomIndex()], Vector3.zero, Quaternion.identity);

            _testSection.SetActive(false);
        }

        private void Update()
        {
            if (!_creatingSection)
            {
                if (_player.horizontalMovement < 0f)
                {
                    _creatingSection = true;

                    _xPositionLeft -= _sectionLong;
                    StartCoroutine(SpawnSection(_xPositionLeft));
                }

                if (_player.horizontalMovement > 0f)
                {
                    _creatingSection = true;

                    _xPositionRight += _sectionLong;
                    StartCoroutine(SpawnSection(_xPositionRight));
                }
            }
        }

        private IEnumerator SpawnSection(float positionX)
        {
            Instantiate(_sections[GetRandomIndex()], new Vector3(positionX, 0f, 0f), Quaternion.identity);

            yield return new WaitForSeconds(_spawnDelay);

            _creatingSection = false;
        }

        private int GetRandomIndex() => Random.Range(0, _sections.Length);
    }
}
