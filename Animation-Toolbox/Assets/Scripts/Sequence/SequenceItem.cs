using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceItem : Toolbox {

    public bool isReadyToSequence;

    public Button animationsTab;
    public Button sequenceTab;
    public GameObject contentImageObj;
    public GameObject iconImageObj;
    public bool stopFlag;

    private Text _textUI;
    private Button _buttonUI;
    private AnimationManager _animationManager;
    private SequenceManager _sequenceManager;

    private void Start() {
        _animationManager = FindObjectOfType<AnimationManager>();
        _sequenceManager = FindObjectOfType<SequenceManager>();

        _textUI = GetComponentInChildren<Text>();

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

        var loadedAsset = AssetsLoader.instance.GetLoadedAsset(this.animationName);
        Sprite[] animationSprites = Array.ConvertAll(loadedAsset, sprites => sprites as Sprite);

        GifferManager gifferManager = GetComponent<GifferManager>();
        gifferManager.SetSingleAnimationEntity(animationName, animationSprites);
        gifferManager.enabled = true;

        contentImageObj.SetActive(true);
        iconImageObj.SetActive(false);

        _textUI.text = animationName;

        ClearRegistrationsOnSetAnimation();
        sequenceTab.onClick.Invoke();
    }

    public override IEnumerator Play() {
        stopFlag = false;
        animTarget.CrossFade(animationName, 0.02f);
        yield break;
    }

    public override IEnumerator PlayAfterAWhile(float delay) {
        stopFlag = false;
        yield return new WaitForSeconds(delay);
        StartCoroutine(Play());
    }

    public override void Stop() {
        stopFlag = true;
        StopAllCoroutines();
    }

    public void ActivateButton() {
        _buttonUI.interactable = true;
    }

    public void DisableButton() {
        _buttonUI.interactable = false;
    }
}
