using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBehaviour : StateMachineBehaviour {

    private AnimationManager _animationManager;
    private AnimationItem _currentAnimationItem;

    private void Awake() {
        _animationManager = FindObjectOfType<AnimationManager>();
    }

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        foreach (var animationItem in _animationManager.animationItems) {
            if (stateInfo.IsName(animationItem.animationName)) {
                _currentAnimationItem = animationItem;
                _currentAnimationItem.RadialProgressBar.SetMaxAmount(stateInfo.length);
                break;
            }
        }
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (_currentAnimationItem != null) {
            _currentAnimationItem.RadialProgressBar.CurrentAmount = _currentAnimationItem.RadialProgressBar.maxAmount * stateInfo.normalizedTime;
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (_currentAnimationItem != null) {
            _currentAnimationItem.RadialProgressBar.CurrentAmount = 0f;
        }
    }

}
