using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SequenceManager : MonoBehaviour {

    public List<SequenceItem> sequenceItems = new List<SequenceItem>();
    public List<Timer> timers = new List<Timer>();

    public Transform content;
    public Button startStopButtonUI;

    private bool _isPlaying;

    public void Initialize() {
        sequenceItems = content.GetComponentsInChildren<SequenceItem>().ToList();
        timers = content.GetComponentsInChildren<Timer>().ToList();

        startStopButtonUI.onClick.AddListener(delegate () {
            if (!_isPlaying)
                StartSequence();
            else
                StopSequence();

            _isPlaying = !_isPlaying;
        });
    }

    public void StartSequence() {
        foreach (Timer timer in timers) {
            StartCoroutine(timer.Play());
        }
        DisableContentButtons();
    }

    public void StopSequence() {
        foreach (Timer timer in timers) {
            timer.Stop();
        }
        ActivateContentButtons();
    }

    public void ActivateContentButtons() {
        foreach (Timer timer in timers) {
            timer.ActivateButtons();
        }

        //Implement sequence items.
    }

    public void DisableContentButtons() {
        foreach (Timer timer in timers) {
            timer.DisableButtons();
        }
        
        //Implement sequence items.
    }

    public void AddToSequence() {

    }

    public void ReplaceItemToSequence() {

    }

}
