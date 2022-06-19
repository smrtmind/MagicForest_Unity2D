using UnityEngine;

namespace Scripts.Player
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private int _coins;

        public int Coins => _coins;

        public void AddCoin(int value) => _coins += value;
    }
}
