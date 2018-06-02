using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolboxItem : MonoBehaviour {

    public delegate void Clicked(ToolboxItem item);
    public Clicked onClicked;

    public Animation animTarget;
    public string animationName;
    public float repeatRate;

    private Text _animationTextUI;
    private RadialProgressBar _radialProgressBar;

    private void Awake() {
        _animationTextUI = GetComponentInChildren<Text>();
        _radialProgressBar = GetComponentInChildren<RadialProgressBar>();
    }

    private void Update() {
        if (_radialProgressBar != null) {
            _radialProgressBar.SetMaxAmount(animTarget[animationName].clip.length);
            _radialProgressBar.CurrentAmount = animTarget[animationName].time;
        }
    }

    public void Init(Animation animTarget, string animationName, float repeatRate) {
        this.animTarget = animTarget;
        this.animationName = animationName;
        this.repeatRate = repeatRate;
        this._animationTextUI.text = animationName;

        GetComponentInChildren<Button>().onClick.AddListener(delegate () {
            Stop();

            if (onClicked != null) {
                onClicked(this);
            }

            StartCoroutine(Play());
        });
    }

    public IEnumerator Play() {
        while (true) {
            animTarget.Play(animationName);
            yield return new WaitForSeconds(repeatRate);
        }
    }

    public IEnumerator PlayAfterAWhile(float delay) {
        yield return new WaitForSeconds(delay);
        StartCoroutine(Play());
    }

    public void Stop() {
        StopAllCoroutines();
    }

    public void SetRepeatRate(float rate) {
        this.repeatRate = rate;
    }
}
