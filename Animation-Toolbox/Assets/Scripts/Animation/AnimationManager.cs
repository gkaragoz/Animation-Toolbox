using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    public List<AnimationItem> animationItems = new List<AnimationItem>();

    public bool debugMode = false;
    [Header("Settings")]
    [Range(1f, 2f)]
    [Tooltip("Only works on debugMode has checked.")]
    public float repeatRate = 1.5f;

    private SnapScrolling _snapScrolling;
    private Animator _targetAnim;

    private void Awake() {
        _snapScrolling = FindObjectOfType<SnapScrolling>();
        _targetAnim = ToolboxManager.instance.targetAnim;
        _targetAnim.GetComponent<ThirdPersonCharacter>().onMovementStarted += StopAllAnimationItems;
    }

    private void Update() {
        if (debugMode) {
            for (int ii = 0; ii < animationItems.Count; ii++) {
                animationItems[ii].SetRepeatRate(repeatRate);
            }
        }
    }

    public List<AnimationItem> Create(AssetPackage[] assetPackages) {
        if (_snapScrolling != null) {
            for (int ii = 0; ii < assetPackages.Length; ii++) {
                AssetPackage assetPackage = assetPackages[ii];

                GameObject newPanel = _snapScrolling.CreateAPanel();

                GifferManager newPanelGifferManager = newPanel.GetComponent<GifferManager>();

                Sprite[] animationSprites = Array.ConvertAll(assetPackage.assets, sprites => sprites as Sprite);

                string animationName = assetPackage.GetAssetName();

                newPanelGifferManager.AddAnimationEntity(animationName, animationSprites);

                AnimationItem animationItem = newPanel.AddComponent<AnimationItem>();
                animationItem.Init(_targetAnim, animationName, repeatRate);
                animationItem.OnClicked += StopAllAnimationItems;
                animationItems.Add(animationItem);

                newPanelGifferManager.enabled = true;
            }
        } else {
            Debug.LogError("Snap Scrolling is null.");
        }
        return animationItems;
    }

    public void StopAllAnimationItems() {
        foreach (AnimationItem animationItem in animationItems) {
            animationItem.Stop();
        }
    }

    public void StopAllAnimationItems(AnimationItem item) {
        foreach (AnimationItem animationItem in animationItems) {
            if (item == animationItem) continue;
            animationItem.Stop();
        }
    }
}
