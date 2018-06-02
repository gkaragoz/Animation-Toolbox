using System;
using System.Collections.Generic;
using UnityEngine;

public class ToolboxManager : MonoBehaviour {

    public bool debugMode = false;

    [Header("Settings")]
    [Range(0.01f, 2f)]
    [Tooltip("Only works on debugMode has checked.")]
    public float repeatRate = 0.5f;
    public Animation animTarget;

    private SnapScrolling _snapScrolling;
    private AssetsLoader _assetsLoader;
    private List<ToolboxItem> _toolboxItems = new List<ToolboxItem>();

    private void Start() {
        _snapScrolling = FindObjectOfType<SnapScrolling>();
        _assetsLoader = FindObjectOfType<AssetsLoader>();
        _assetsLoader.onAssetsLoaded += OnAssetsLoaded;
        _assetsLoader.LoadAssets(typeof(Sprite));
    }

    private void Update() {
        if (debugMode) {
            for (int ii = 0; ii < _toolboxItems.Count; ii++) {
                _toolboxItems[ii].SetRepeatRate(repeatRate);
            }
        }
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

                ToolboxItem toolboxItem = newPanel.GetComponent<ToolboxItem>();
                toolboxItem.Init(animTarget, animationName, repeatRate);
                toolboxItem.onClicked += StopAll;
                _toolboxItems.Add(toolboxItem);

                newPanelAnimationManager.enabled = true;
            }
        } else {
            Debug.LogError("Assets Loader or Snap Scrolling is null.");
        }
    }

    private void StopAll(ToolboxItem item) {
        foreach (ToolboxItem toolboxItem in _toolboxItems) {
            if (item == toolboxItem) continue;
            toolboxItem.Stop();
        }
    }

}
