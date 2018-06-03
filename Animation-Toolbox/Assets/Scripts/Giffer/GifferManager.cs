using UnityEngine;
using System.Collections.Generic;

public class GifferManager : GifferController {
    [System.Serializable]
    public class AnimationEntity {
        public string name;
        public Sprite[] sprites;

        public AnimationEntity(string name, Sprite[] sprites) {
            this.name = name;
            this.sprites = sprites;
        }
    }

    [Header("Initialization")]
    public List<AnimationEntity> animationEntities = new List<AnimationEntity>();

    private AnimationEntity _selectedAnimation;

    void Start() {
        StartAnimation();
    }

    public void SetSingleAnimationEntity(string name, Sprite[] sprites) {
        animationEntities = new List<AnimationEntity>();
        animationEntities.Add(new AnimationEntity(name, sprites));

        StopAnimation();
        StartAnimation();
    }

    public void AddAnimationEntity(string name, Sprite[] sprites) {
        animationEntities.Add(new AnimationEntity(name, sprites));
    }

    public void StartAnimation() {
        _selectedAnimation = animationEntities[0];
        Play(_selectedAnimation.sprites);
    }

    public void StopAnimation() {
        Stop();
    }

    public string GetSelectedAnimationName() {
        return _selectedAnimation.name;
    }
}