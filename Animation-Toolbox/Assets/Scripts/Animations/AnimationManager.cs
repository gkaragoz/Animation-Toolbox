﻿using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AnimationEntity {
    public string name;
    public Sprite[] sprites;

    public AnimationEntity(string name, Sprite[] sprites) {
        this.name = name;
        this.sprites = sprites;
    }
}

public class AnimationManager : AnimationController {

    [Header("Initialization")]
    public List<AnimationEntity> animationEntities = new List<AnimationEntity>();

    private AnimationEntity _selectedAnimation;

    void Start() {
        _selectedAnimation = animationEntities[0];
        StartAnimation();
    }

    public void AddAnimationEntity(string name, Sprite[] sprites) {
        animationEntities.Add(new AnimationEntity(name, sprites));
    }

    public void StartAnimation() {
        Play(_selectedAnimation.sprites);
    }

    public void StopAnimation() {
        Stop();
    }
}