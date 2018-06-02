using System;
using UnityEngine;
using UnityEngine.UI;

public class ToolboxManager : MonoBehaviour {

    public Animation animTarget;

    private SnapScrolling _snapScrolling;
    private AssetsLoader _assetsLoader;

    private void Start() {
        _snapScrolling = FindObjectOfType<SnapScrolling>();
        _assetsLoader = FindObjectOfType<AssetsLoader>();
        _assetsLoader.onAssetsLoaded += OnAssetsLoaded;
        _assetsLoader.LoadAssets(typeof(Sprite));
    }

    private void OnAssetsLoaded() {
        if (_assetsLoader != null && _snapScrolling != null) {
            for (int ii = 0; ii < _assetsLoader.assetPackages.Length; ii++) {
                AssetPackage assetPackage = _assetsLoader.assetPackages[ii];

                GameObject newPanel = _snapScrolling.CreateAPanel();
                AnimationManager newPanelAnimationManager = newPanel.GetComponent<AnimationManager>();

                Sprite[] animationSprites = Array.ConvertAll(assetPackage.assets, sprites => sprites as Sprite);

                string animationName = assetPackage.GetLeafFolderName();

                newPanelAnimationManager.AddAnimationEntity(animationName, animationSprites);

                newPanel.GetComponentInChildren<Button>().onClick.AddListener(delegate () {
                    animTarget.Play(animationName);
                });

                newPanelAnimationManager.enabled = true;
            }
        } else {
            Debug.LogError("Assets Loader or Snap Scrolling is null.");
        }
    }

}
