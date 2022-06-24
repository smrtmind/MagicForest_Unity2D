using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Utils
{
    public class GameManager : MonoBehaviour
    {
        private LoadAssetBundles _bundleLoader;
        private bool _sceneIsLoaded;

        private GameObject _bundle;
        public GameObject Bundle
        {
            get => _bundle;
            set => _bundle = value;
        }

        private void Awake()
        {
            if (GameObject.FindGameObjectsWithTag(gameObject.tag).Length > 1)
                Destroy(gameObject);
            else
                DontDestroyOnLoad(this);

            _bundleLoader = FindObjectOfType<LoadAssetBundles>();
        }

        private void Update()
        {
            var scene = SceneManager.GetActiveScene().name;
            if (scene == "EndlessLevel")
            {
                if (!_sceneIsLoaded)
                {
                    Instantiate(_bundle);
                    _sceneIsLoaded = true;
                }
            }
            if (scene == "MainMenu")
                _sceneIsLoaded = false;
        }
    }
}
