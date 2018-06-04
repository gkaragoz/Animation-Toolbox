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

    private int sequenceItemsIndex = 0;
    private int timersIndex = 0;

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
        });
    }

    public void StartSequence() {
        _isPlaying = !_isPlaying;
        startStopButtonUI.transform.Find("Icon").GetComponent<Image>().sprite = stopButtonSprite;
        DisableContentButtons();

        PlayNextTimer();
    }

    public void PlayNextTimer() {
        if (timersIndex >= timers.Count) {
            StopSequence();
            return;
        }

        timers[timersIndex].OnTimerFinished += OnTimerFinished;
        StartCoroutine(timers[timersIndex].Play());
    }

    public void PlayNextSequenceItem() {
        if (sequenceItemsIndex >= sequenceItems.Count) {
            StopSequence();
            return;
        }

        StartCoroutine(sequenceItems[sequenceItemsIndex].Play());
    }

    public void OnSequenceItemFinish() {
        sequenceItemsIndex++;
        PlayNextTimer();
    }

    public void OnTimerFinished() {
        timers[timersIndex].OnTimerFinished -= OnTimerFinished;
        timersIndex++;
        PlayNextSequenceItem();
    }

    public void StopSequence() {
        _isPlaying = !_isPlaying;
        startStopButtonUI.transform.Find("Icon").GetComponent<Image>().sprite = startButtonSprite;
        timersIndex = 0;
        sequenceItemsIndex = 0;

        foreach (Timer timer in timers) {
            timer.Stop();
        }

        foreach (SequenceItem sequenceItem in sequenceItems) {
            sequenceItem.Stop();
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

}
