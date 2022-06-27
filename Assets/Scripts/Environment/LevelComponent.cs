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

        private PlayerController _player;
        private float _xPositionRight;
        private float _xPositionLeft;
        private bool _creatingSection = false;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>();
        }

        private void Start()
        {
            //spawn first random section on start in the middle of the scene, to make every start of game unique
            Instantiate(_sections[GetRandomIndex()], Vector3.zero, Quaternion.identity);
        }

        private void Update()
        {
            if (!_creatingSection)
            {
                //create new sections if player moves left
                if (_player.leftPressed)//_player.horizontalMovement < 0f (for PC build)
                {
                    _creatingSection = true;

                    _xPositionLeft -= _sectionLong;
                    StartCoroutine(SpawnSection(_xPositionLeft));
                }

                //create new sections if player moves right
                if (_player.rightPressed)//_player.horizontalMovement > 0f (for PC build)
                {
                    _creatingSection = true;

                    _xPositionRight += _sectionLong;
                    StartCoroutine(SpawnSection(_xPositionRight));
                }
            }
        }

        private IEnumerator SpawnSection(float positionX)
        {
            //choose random section from the array of ready sections
            Instantiate(_sections[GetRandomIndex()], new Vector3(positionX, 0f, 0f), Quaternion.identity);

            yield return new WaitForSeconds(_spawnDelay);

            _creatingSection = false;
        }

        private int GetRandomIndex() => Random.Range(0, _sections.Length);
    }
}
