using System.Collections.Generic;
using UnityEngine;

public class ToolboxManager : MonoBehaviour {
    public static ToolboxManager instance;

    [Header("Initialize")]
    public Animator targetAnim;

    [HideInInspector]
    public List<AnimationItem> animationItems = new List<AnimationItem>();
    [HideInInspector]
    public List<SequenceItem> sequenceItems = new List<SequenceItem>();

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
        animationItems = _animationManager.Create(assetPackages);
        sequenceItems = _sequenceManager.Initialize();
    }
}
