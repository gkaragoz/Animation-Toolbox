using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Toolbox : MonoBehaviour {
    public Animator animTarget;
    public string animationName;

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

    public virtual void Update() {
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
}
