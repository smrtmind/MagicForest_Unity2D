using System.IO;
using UnityEditor;

public class CreateAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAssetBundles()
    {
        string AssetBundlesDirectory = "Assets/AssetBundles";

        if (!Directory.Exists(AssetBundlesDirectory))
            Directory.CreateDirectory(AssetBundlesDirectory);

        BuildPipeline.BuildAssetBundles(AssetBundlesDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}
