using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssetPackage {
    public string folderName;
    public Object[] assets;

    public string GetLeafFolderName() {
        string[] splittedFolderName = folderName.Split('/');
        int lastWordIndex = splittedFolderName.Length - 1;

        return splittedFolderName[lastWordIndex];
    }
}

public class AssetsLoader : MonoBehaviour {

    public delegate void AssetLoaderEventHandler(AssetPackage[] assetPackages);
    public event AssetLoaderEventHandler OnAssetsLoaded;

    [Header("Initialization")]
    public AssetPackage[] assetPackages;

    public void LoadAssets(System.Type type) {
        for (int ii = 0; ii < assetPackages.Length; ii++) {
            string folderName = assetPackages[ii].folderName;
            assetPackages[ii].assets = Resources.LoadAll(folderName, type);
        }

        if (OnAssetsLoaded != null) {
            OnAssetsLoaded.Invoke(assetPackages);
        }
    }

    public Object[] GetLoadedAsset(string folderName) {
        foreach (AssetPackage package in assetPackages) {
            if (package.folderName == folderName) {
                return package.assets;
            }
        }
        return null;
    }
	
}
