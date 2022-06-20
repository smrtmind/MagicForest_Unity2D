using UnityEngine;

namespace Scripts.Environment
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private float _parallaxEffect;

        private Camera _camera;
        private float _length;
        private float _startPosition;

        private void Awake()
        {
            _camera = FindObjectOfType<Camera>();
            _startPosition = transform.position.x;
            _length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void FixedUpdate()
        {
            var temp = _camera.transform.position.x * (1 - _parallaxEffect);
            var distance = _camera.transform.position.x * _parallaxEffect;

            transform.position = new Vector3(_startPosition + distance, transform.position.y, transform.position.z);

            if (temp > _startPosition + _length)
                _startPosition += _length;
            else if (temp < _startPosition - _length)
                _startPosition -= _length;
        }
    }
}
