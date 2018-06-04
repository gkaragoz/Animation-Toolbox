using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceBehaviour : StateMachineBehaviour {

    private SequenceManager _sequenceManager;
    private SequenceItem _currentSequenceItem;

    private void Awake() {
        _sequenceManager = FindObjectOfType<SequenceManager>();
    }

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        foreach (var sequenceItem in _sequenceManager.sequenceItems) {
            if (stateInfo.IsName(sequenceItem.animationName)) {
                _currentSequenceItem = sequenceItem;
                _currentSequenceItem.RadialProgressBar.SetMaxAmount(stateInfo.length);
                break;
            }
        }
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (_currentSequenceItem != null) {
            if (_currentSequenceItem.stopFlag == true) {
                _currentSequenceItem.RadialProgressBar.CurrentAmount = 0f;
            } else {
                _currentSequenceItem.RadialProgressBar.CurrentAmount = _currentSequenceItem.RadialProgressBar.maxAmount* stateInfo.normalizedTime;
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (_currentSequenceItem != null) {
            if (_currentSequenceItem.stopFlag == false) {
                _currentSequenceItem.RadialProgressBar.CurrentAmount = 0f;
                _sequenceManager.OnSequenceItemFinish();
            }
        }
    }

}
