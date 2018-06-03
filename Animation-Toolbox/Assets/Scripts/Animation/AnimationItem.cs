using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimationItem : Toolbox {
    public delegate void ClickEventHandler(AnimationItem item);
    public event ClickEventHandler OnClicked;

    public delegate void SetAnimationEventHandler(Animator animTarget, string animationName);
    public event SetAnimationEventHandler OnSetAnimation;

    public float repeatRate;
    
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

            switch (ToolboxManager.instance.currentState) {
                case ToolboxManager.TabState.Animations:
                StartCoroutine(Play());
                break;
                case ToolboxManager.TabState.Sequence:
                break;
                case ToolboxManager.TabState.AnimationSelectionForSequence:
                if (OnSetAnimation != null) {
                    OnSetAnimation.Invoke(animTarget, animationName);
                }
                break;
                default:
                Debug.LogError("There is no such a state.");
                break;
            }
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
