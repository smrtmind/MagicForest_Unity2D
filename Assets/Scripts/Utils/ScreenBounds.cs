using UnityEngine;

namespace Scripts.Utils
{
    public class ScreenBounds : MonoBehaviour
    {
        public Bounds Bounds { get; private set; }
        public Bounds borderOfBounds { get; private set; }

        private Camera _camera;
        private Vector3 _screenMin;
        private Vector3 _screenMax;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            _screenMin = _camera.ViewportToWorldPoint(Vector3.zero);
            _screenMax = _camera.ViewportToWorldPoint(new Vector3(1.3f, 1.3f, 1.3f));

            var center = _camera.transform.position;
            Bounds = InitializeBounds(center, _screenMin, _screenMax);

            var border = Vector3.one;
            borderOfBounds = InitializeBounds(center, _screenMax - border, _screenMin + border);
        }

        private Bounds InitializeBounds(Vector3 center, Vector3 min, Vector3 max)
        {
            return new Bounds(center, new Vector3(max.x - min.x, max.y - min.y));
        }
    }
}
