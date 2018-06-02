using System;
using UnityEngine;

public class ToolboxManager : MonoBehaviour {

    private SnapScrolling _snapScrolling;
    private AssetsLoader _assetsLoader;

    private void Awake() {
        _snapScrolling = FindObjectOfType<SnapScrolling>();
        _assetsLoader = FindObjectOfType<AssetsLoader>();

        _assetsLoader.LoadAssets(typeof(Sprite));
    }

    private void Start() {
        if (_assetsLoader != null && _snapScrolling != null) {
            for (int ii = 0; ii < _assetsLoader.assetPackages.Length; ii++) {
                AssetPackage assetPackage = _assetsLoader.assetPackages[ii];

                AnimationManager newPanelAnimationManager = _snapScrolling.CreateAPanel().GetComponent<AnimationManager>();
                Sprite[] animationSprites = Array.ConvertAll(assetPackage.assets, sprites => sprites as Sprite);
                newPanelAnimationManager.AddAnimationEntity(assetPackage.folderName, animationSprites);
                newPanelAnimationManager.enabled = true;
            }
        } else {
            Debug.LogError("Assets Loader or Snap Scrolling is null.");
        }
    }

}
