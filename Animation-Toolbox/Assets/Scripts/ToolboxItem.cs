﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class ToolboxItem : MonoBehaviour {
    public Animator animTarget;
    public string animationName;
    public float repeatRate;

    protected Text AnimationTextUI { get; set; }
    protected RadialProgressBar RadialProgressBar { get; set; }

    public delegate void ClickEventHandler(AnimationItem item);
    public abstract event ClickEventHandler OnClicked;

    public abstract IEnumerator Play();
    public abstract IEnumerator PlayAfterAWhile(float delay);
    public abstract void Stop();

    private void Awake() {
        AnimationTextUI = GetComponentInChildren<Text>();
        RadialProgressBar = GetComponentInChildren<RadialProgressBar>();
    }
}
