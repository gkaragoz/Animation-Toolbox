using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsLoader : MonoBehaviour {

    [System.Serializable]
    public class AssetPackage {
        public string folderName;
        public Object[] assets;
    }

    [Header("Initialization")]
    public AssetPackage[] assetPackages;

	void Awake () {
        LoadAssets();
    }

    void LoadAssets() {
        for (int ii = 0; ii < assetPackages.Length; ii++) {
            string folderName = assetPackages[ii].folderName;
            assetPackages[ii].assets = Resources.LoadAll(folderName);
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
