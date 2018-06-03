using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SequenceManager : MonoBehaviour {

    public List<SequenceItem> sequenceItems = new List<SequenceItem>();

    public List<SequenceItem> Initialize() {
        sequenceItems = FindObjectsOfType<SequenceItem>().ToList();
        return sequenceItems;
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
