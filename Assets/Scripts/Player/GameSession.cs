using Scripts.Utils;
using UnityEngine;

namespace Scripts.Player
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private int _coins;
        [SerializeField] private bool _gameOver;

        [Space]
        [SerializeField] private ObjectSpawner[] _spawners;

        public int Coins => _coins;
        public bool GameOver => _gameOver;

        public void AddCoin(int value) => _coins += value;

        public void GameIsOver()
        {
            _gameOver = true;
            DisableSpawners();
        }

        public void DisableSpawners()
        {
            foreach (var spawner in _spawners)
                spawner.StartSpawn = false;
        }
    }
}
