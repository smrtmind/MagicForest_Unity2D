using UnityEngine;

namespace Scripts.Utils
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private bool _isTouchingLayer;
        [SerializeField] private float _beamLength = 1f;
        [SerializeField] private Color _color = Color.red;

        public bool IsTouchingLayer => _isTouchingLayer;

        private Vector3 _raycastDirection;

        private void Update()
        {
            _isTouchingLayer = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), _beamLength, 1 << 6);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * _beamLength, _color);
        }
    }
}
