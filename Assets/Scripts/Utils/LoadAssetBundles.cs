using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Scripts.Utils
{
    public class LoadAssetBundles : MonoBehaviour
    {
        [SerializeField] private string _bundleUrl;
        [SerializeField] private string _assetToLoadName;
        [SerializeField] private uint _version = 0;
        
        private GameManager _gameManager;

        private bool _downloadComplete;
        public bool DownloadComplete => _downloadComplete;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        public void DownloadAssetBundles() => StartCoroutine(DownloadAndCache());

        private IEnumerator DownloadAndCache()
        {
            while (!Caching.ready)
                yield return null;

            using (var www = UnityWebRequestAssetBundle.GetAssetBundle(_bundleUrl, _version))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    // Get downloaded asset bundle
                    var remoteBundle = DownloadHandlerAssetBundle.GetContent(www);
                    _gameManager.Bundle = (GameObject)remoteBundle.LoadAsset(_assetToLoadName);
                    remoteBundle.Unload(false);

                    _downloadComplete = true;
                }
            }
        }
    }
}
