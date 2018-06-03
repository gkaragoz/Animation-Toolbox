using System;
using System.Collections.Generic;
using System.Linq;
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
    private List<SequenceItem> _sequenceItems = new List<SequenceItem>();

    private void Start() {
        _snapScrolling = FindObjectOfType<SnapScrolling>();
        _assetsLoader = FindObjectOfType<AssetsLoader>();
        _assetsLoader.OnAssetsLoaded += OnAssetsLoaded;
        _assetsLoader.LoadAssets(typeof(Sprite));

        ThirdPersonCharacter character = animTarget.GetComponent<ThirdPersonCharacter>();
        character.onMovementStarted += StopAll;

        InitializeSequenceItems();
    }

    private void Update() {
        if (debugMode) {
            for (int ii = 0; ii < _animationItems.Count; ii++) {
                _animationItems[ii].SetRepeatRate(repeatRate);
            }
        }
    }

    private void OnAssetsLoaded(AssetPackage[] assetPackages) {
        if (_snapScrolling != null) {
            CreateAnimationItems(assetPackages);
        } else {
            Debug.LogError("Snap Scrolling is null.");
        }
    }

    private void CreateAnimationItems(AssetPackage[] assetPackages) {
        for (int ii = 0; ii < assetPackages.Length; ii++) {
            AssetPackage assetPackage = assetPackages[ii];

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
    }

    private void InitializeSequenceItems() {
        _sequenceItems = FindObjectsOfType<SequenceItem>().ToList();
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
