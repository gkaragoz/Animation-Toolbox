using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssetPackage {
    public string folderName;
    public Object[] assets;
}

public class AssetsLoader : MonoBehaviour {

    [Header("Initialization")]
    public AssetPackage[] assetPackages;

    public void LoadAssets(System.Type type) {
        for (int ii = 0; ii < assetPackages.Length; ii++) {
            string folderName = assetPackages[ii].folderName;
            assetPackages[ii].assets = Resources.LoadAll(folderName, type);
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
