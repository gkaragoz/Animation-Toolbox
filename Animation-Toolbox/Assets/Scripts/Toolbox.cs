using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Toolbox : MonoBehaviour {
    public Animator animTarget;
    public string animationName;
    public RadialProgressBar RadialProgressBar { get; set; }

    protected Text AnimationTextUI { get; set; }

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
