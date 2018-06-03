using System.Collections.Generic;
using UnityEngine;

public class ToolboxManager : MonoBehaviour {
    public static ToolboxManager instance;

    public enum TabState {
        Animations = 0,
        Sequence = 1,
        AnimationSelectionForSequence = 2
    }

    [Header("Initialize")]
    public Animator targetAnim;
    public TabState currentState;

    private AnimationManager _animationManager;
    private SequenceManager _sequenceManager;
    private AssetsLoader _assetsLoader;

    private void Awake() {
        if (instance == null)
            instance = this;

        _animationManager = GetComponent<AnimationManager>();
        _sequenceManager = GetComponent<SequenceManager>();
    }

    private void Start() {
        _assetsLoader = FindObjectOfType<AssetsLoader>();
        _assetsLoader.OnAssetsLoaded += OnAssetsLoaded;
        _assetsLoader.LoadAssets(typeof(Sprite));
    }

    private void OnAssetsLoaded(AssetPackage[] assetPackages) {
        _animationManager.Create(assetPackages);
        _sequenceManager.Initialize();
    }

    public void SetState(TabState state) {
        this.currentState = state;
    }

    // Unity didn't implement this enum attribute on editor inspector for any kind of trigger event paramaters. This is why I'm using int overload method.
    public void SetState(int state) {
        this.currentState = (TabState)state;
    }
}
