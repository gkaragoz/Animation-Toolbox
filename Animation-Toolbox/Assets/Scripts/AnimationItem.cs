using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationItem : ToolboxItem {
    public override Animator AnimTarget { get; set; }
    public override string AnimationName { get; set; }
    public override float RepeatRate { get; set; }
    public override Text AnimationTextUI { get; set; }
    public override RadialProgressBar RadialProgressBar { get; set; }

    public override event ClickEventHandler OnClicked;

    private void Awake() {
        AnimationTextUI = GetComponentInChildren<Text>();
        RadialProgressBar = GetComponentInChildren<RadialProgressBar>();
    }

    private void Update() {
        if (RadialProgressBar != null) {
            AnimatorStateInfo animationState = AnimTarget.GetCurrentAnimatorStateInfo(0);
            AnimatorClipInfo[] myAnimatorClip = AnimTarget.GetCurrentAnimatorClipInfo(0);

            if (animationState.IsName(AnimationName)) {
                float myTime = myAnimatorClip[0].clip.length * animationState.normalizedTime;
                RadialProgressBar.SetMaxAmount(myAnimatorClip[0].clip.length);
                RadialProgressBar.CurrentAmount = myTime;
            }
        }
    }

    public void Init(Animator animTarget, string animationName, float repeatRate) {
        this.AnimTarget = animTarget;
        this.AnimationName = animationName;
        this.RepeatRate = repeatRate;
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
            AnimTarget.CrossFade(AnimationName, 0.02f);
            yield return new WaitForSeconds(RepeatRate);
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
        this.RepeatRate = rate;
    }
}
