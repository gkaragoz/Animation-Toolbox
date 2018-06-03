using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationItem : ToolboxItem {
    public override event ClickEventHandler OnClicked;

    private void Update() {
        if (RadialProgressBar != null) {
            AnimatorStateInfo animationState = animTarget.GetCurrentAnimatorStateInfo(0);
            AnimatorClipInfo[] myAnimatorClip = animTarget.GetCurrentAnimatorClipInfo(0);

            if (animationState.IsName(animationName)) {
                float myTime = myAnimatorClip[0].clip.length * animationState.normalizedTime;
                RadialProgressBar.SetMaxAmount(myAnimatorClip[0].clip.length);
                RadialProgressBar.CurrentAmount = myTime;
            }
        }
    }

    public void Init(Animator animTarget, string animationName, float repeatRate) {
        this.animTarget = animTarget;
        this.animationName = animationName;
        this.repeatRate = repeatRate;
        this.AnimationTextUI.text = animationName;

        GetComponentInChildren<Button>().onClick.AddListener(delegate () {
            Stop();

            if (OnClicked != null) {
                OnClicked.Invoke(this);
            }

            StartCoroutine(Play());
        });
    }

    public override IEnumerator Play() {
        while (true) {
            animTarget.CrossFade(animationName, 0.02f);
            yield return new WaitForSeconds(repeatRate);
        }
    }

    public override IEnumerator PlayAfterAWhile(float delay) {
        yield return new WaitForSeconds(delay);
        StartCoroutine(Play());
    }

    public override void Stop() {
        RadialProgressBar.ResetAmount();
        StopAllCoroutines();
    }

    public void SetRepeatRate(float rate) {
        this.repeatRate = rate;
    }
}
