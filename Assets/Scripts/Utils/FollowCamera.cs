using Scripts.Player;
using UnityEngine;

namespace Scripts.Utils
{
    public class FollowCamera : MonoBehaviour
    {
        private Transform _player;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        }

        private void Update()
        {
            transform.position = new Vector3(_player.position.x, transform.position.y, transform.position.z);
        }
    }
}
