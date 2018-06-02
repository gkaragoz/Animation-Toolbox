using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolboxManager : MonoBehaviour {

    private SnapScrolling _snapScrolling;
    private AssetsLoader _assetsLoader;

    private void Awake() {
        _snapScrolling = FindObjectOfType<SnapScrolling>();
        _assetsLoader = FindObjectOfType<AssetsLoader>();
    }

    private void Start() {
        for (int ii = 0; ii < 10; ii++) {
            _snapScrolling.CreateAPanel();
        }
    }
}
