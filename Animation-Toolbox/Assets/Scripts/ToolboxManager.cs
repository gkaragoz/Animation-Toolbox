using System;
using System.Collections.Generic;
using UnityEngine;

public class ToolboxManager : MonoBehaviour {

    public bool debugMode = false;

    [Header("Settings")]
    [Range(0.01f, 2f)]
    [Tooltip("Only works on debugMode has checked.")]
    public float repeatRate = 0.5f;
    public Animator animTarget;

    private SnapScrolling _snapScrolling;
    private AssetsLoader _assetsLoader;
    private List<AnimationItem> _animationItems = new List<AnimationItem>();

    private void Start() {
        _snapScrolling = FindObjectOfType<SnapScrolling>();
        _assetsLoader = FindObjectOfType<AssetsLoader>();
        _assetsLoader.onAssetsLoaded += OnAssetsLoaded;
        _assetsLoader.LoadAssets(typeof(Sprite));

        ThirdPersonCharacter character = animTarget.GetComponent<ThirdPersonCharacter>();
        character.onMovementStarted += StopAll;
    }

    private void Update() {
        if (debugMode) {
            for (int ii = 0; ii < _animationItems.Count; ii++) {
                _animationItems[ii].SetRepeatRate(repeatRate);
            }
        }
    }

    private void OnAssetsLoaded() {
        if (_assetsLoader != null && _snapScrolling != null) {
            for (int ii = 0; ii < _assetsLoader.assetPackages.Length; ii++) {
                AssetPackage assetPackage = _assetsLoader.assetPackages[ii];

                GameObject newPanel = _snapScrolling.CreateAPanel();
                GifferManager newPanelAnimationManager = newPanel.GetComponent<GifferManager>();

                Sprite[] animationSprites = Array.ConvertAll(assetPackage.assets, sprites => sprites as Sprite);

                string animationName = assetPackage.GetLeafFolderName();

                newPanelAnimationManager.AddAnimationEntity(animationName, animationSprites);

                AnimationItem animationItem = newPanel.AddComponent<AnimationItem>();
                animationItem.Init(animTarget, animationName, repeatRate);
                animationItem.OnClicked += StopAll;
                _animationItems.Add(animationItem);

                newPanelAnimationManager.enabled = true;
            }
        } else {
            Debug.LogError("Assets Loader or Snap Scrolling is null.");
        }
    }

    private void StopAll() {
        foreach (AnimationItem animationItem in _animationItems) {
            animationItem.Stop();
        }
    }

    public void StopAll(AnimationItem item) {
        foreach (AnimationItem animationItem in _animationItems) {
            if (item == animationItem) continue;
            animationItem.Stop();
        }
    }

}
