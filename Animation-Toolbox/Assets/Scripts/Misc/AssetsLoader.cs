using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssetPackage {
    public string folderPath;
    public Object[] assets;

    public string GetAssetName() {
        string[] splittedFolderName = folderPath.Split('/');
        int lastWordIndex = splittedFolderName.Length - 1;

        return splittedFolderName[lastWordIndex];
    }
}

public class AssetsLoader : MonoBehaviour {

    public static AssetsLoader instance;

    public delegate void AssetLoaderEventHandler(AssetPackage[] assetPackages);
    public event AssetLoaderEventHandler OnAssetsLoaded;

    [Header("Initialization")]
    public AssetPackage[] assetPackages;

    private void Awake() {
        if (instance == null)
            instance = this;
    }

    public void LoadAssets(System.Type type) {
        for (int ii = 0; ii < assetPackages.Length; ii++) {
            string folderName = assetPackages[ii].folderPath;
            assetPackages[ii].assets = Resources.LoadAll(folderName, type);
        }

        if (OnAssetsLoaded != null) {
            OnAssetsLoaded.Invoke(assetPackages);
        }
    }

    public Object[] GetLoadedAsset(string assetName) {
        foreach (AssetPackage package in assetPackages) {
            if (package.GetAssetName() == assetName) {
                return package.assets;
            }
        }
        return null;
    }
}
