using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SequenceManager : MonoBehaviour {

    public List<SequenceItem> sequenceItems = new List<SequenceItem>();
    public List<Timer> timers = new List<Timer>();

    public Transform content;

    public void Initialize() {
        sequenceItems = content.GetComponentsInChildren<SequenceItem>().ToList();
        timers = content.GetComponentsInChildren<Timer>().ToList();
    }

    public void StartSequence() {

    }

    public void StopSequence() {

    }

    public void AddToSequence() {

    }

    public void ReplaceItemToSequence() {

    }

}
