using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class ToolboxItem : MonoBehaviour {
    public abstract Animator AnimTarget { get; set; }
    public abstract string AnimationName { get; set; }
    public abstract float RepeatRate { get; set; }
    public abstract Text AnimationTextUI { get; set; }
    public abstract RadialProgressBar RadialProgressBar { get; set; }

    public delegate void ClickEventHandler(AnimationItem item);
    public abstract event ClickEventHandler OnClicked;

    public abstract IEnumerator Play();
    public abstract IEnumerator PlayAfterAWhile(float delay);
    public abstract void Stop();
}
