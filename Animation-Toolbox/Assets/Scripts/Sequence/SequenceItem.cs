﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceItem : Toolbox {

    public bool isReadyToSequence;

    public Button animationsTab;
    public Button sequenceTab;

    private Button _buttonUI;
    private AnimationManager _animationManager;
    private SequenceManager _sequenceManager;

    private void Awake() {
        _animationManager = FindObjectOfType<AnimationManager>();
        _sequenceManager = FindObjectOfType<SequenceManager>();

        _buttonUI = GetComponentInChildren<Button>();
        _buttonUI.onClick.AddListener(delegate () {
            if (!isReadyToSequence) {
                animationsTab.onClick.Invoke();
                ToolboxManager.instance.SetState(ToolboxManager.TabState.AnimationSelectionForSequence);
                _sequenceManager.SelectedItem = this;
            }
        });
    }

    public void RegisterOnSetAnimation() {
        foreach (AnimationItem animationItem in _animationManager.animationItems) {
            animationItem.OnSetAnimation += InitializeSequenceItem;
        }
    }

    public void ClearRegistrationsOnSetAnimation() {
        foreach (AnimationItem animationItem in _animationManager.animationItems) {
            animationItem.OnSetAnimation -= InitializeSequenceItem;
        }
    }

    public void InitializeSequenceItem(Animator animator, string animationName) {
        this.animTarget = animator;
        this.animationName = animationName;

        ClearRegistrationsOnSetAnimation();
        sequenceTab.onClick.Invoke();
    }

    public override IEnumerator Play() {
        animTarget.CrossFade(animationName, 0.02f);
        yield break;
    }

    public override IEnumerator PlayAfterAWhile(float delay) {
        yield return new WaitForSeconds(delay);
        StartCoroutine(Play());
    }

    public override void Stop() {
        RadialProgressBar.ResetAmount();
        StopAllCoroutines();
    }
}