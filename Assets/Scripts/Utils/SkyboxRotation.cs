using UnityEngine;

namespace Scripts.Utils
{
    public class SkyboxRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 1f;

        private void Update()
        {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * _rotationSpeed);
        }
    }
}
