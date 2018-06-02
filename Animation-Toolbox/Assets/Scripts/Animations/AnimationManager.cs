using UnityEngine;

[System.Serializable]
public class AnimationEntity {
    public string name;
    public Sprite[] sprites;
}

public class AnimationManager : AnimationController {

    [Header("Initialization")]
    public AnimationEntity[] animationEntities;

    private AnimationEntity _selectedAnimation;

    void Start() {
        _selectedAnimation = animationEntities[0];
        StartAnimation();
    }

    public void StartAnimation() {
        Play(_selectedAnimation.sprites);
    }

    public void StopAnimation() {
        Stop();
    }
}