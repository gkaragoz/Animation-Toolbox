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
    public Sprite startButtonSprite;
    public Sprite stopButtonSprite;

    private SequenceItem _selectedItem;
    public SequenceItem SelectedItem {
        get {
            return _selectedItem;
        }
        set {
            _selectedItem = value;
            _selectedItem.RegisterOnSetAnimation();
        }
    }

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
        startStopButtonUI.transform.Find("Icon").GetComponent<Image>().sprite = stopButtonSprite;

        foreach (Timer timer in timers) {
            StartCoroutine(timer.Play());
        }
        DisableContentButtons();
    }

    public void StopSequence() {
        startStopButtonUI.transform.Find("Icon").GetComponent<Image>().sprite = startButtonSprite;

        foreach (Timer timer in timers) {
            timer.Stop();
        }
        ActivateContentButtons();
    }

    public void ActivateContentButtons() {
        foreach (Timer timer in timers) {
            timer.ActivateButtons();
        }

        foreach (SequenceItem sequenceItem in sequenceItems) {
            sequenceItem.ActivateButton();
        }
    }

    public void DisableContentButtons() {
        foreach (Timer timer in timers) {
            timer.DisableButtons();
        }

        foreach (SequenceItem sequenceItem in sequenceItems) {
            sequenceItem.DisableButton();
        }
    }

    public void AddToSequence() {

    }

    public void ReplaceItemToSequence() {

    }

}
