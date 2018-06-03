using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : Toolbox {
    public delegate void TimerFinishEventHandler(Timer timer);
    public event TimerFinishEventHandler OnTimerFinished;

    [SerializeField]
    private float _currentValue;
    public float CurrentValue {
        get {
            return _currentValue;
        }
        set {
            _currentValue = value;
            if (_currentValue < 0f) {
                _currentValue = 0f;

                if (OnTimerFinished != null)
                    OnTimerFinished.Invoke(this);
            }
            _valueTextUI.text = _currentValue.ToString("F1");
            RadialProgressBar.CurrentAmount = _currentValue;
        }
    }

    [SerializeField]
    private float _maxValue;
    public float MaxValue {
        get {
            return _maxValue;
        }
        set {
            _maxValue = value;
            if (_maxValue < 0)
                _maxValue = 0;

            _valueTextUI.text = _maxValue.ToString("F1");
            SetRadialProgressBar();
        }
    }

    private const float ADDITION_VALUE = 0.5f;
    private Text _valueTextUI;
    private Button _plusButtonUI;
    private Button _minusButtonUI;

    private void Start() {
        _valueTextUI = GetComponentInChildren<Text>();
        _plusButtonUI = transform.Find("Btn_Plus").GetComponent<Button>();
        _minusButtonUI = transform.Find("Btn_Minus").GetComponent<Button>();

        _plusButtonUI.onClick.AddListener(delegate () {
            if (_plusButtonUI.IsInteractable()) {
                PlusClick();
            }
        });

        _minusButtonUI.onClick.AddListener(delegate () {
            if (_minusButtonUI.IsInteractable()) {
                MinusClick();
            }
        });

        CurrentValue = 0f;
    }

    private void SetRadialProgressBar() {
        RadialProgressBar.SetMaxAmount(MaxValue);
        RadialProgressBar.CurrentAmount = 0f;
    }
    
    public override IEnumerator Play() {
        CurrentValue = MaxValue;
        while (CurrentValue > 0) {
            CurrentValue -= Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }

    public override IEnumerator PlayAfterAWhile(float delay) {
        CurrentValue = MaxValue;
        yield return new WaitForSeconds(delay);
        while (CurrentValue > 0) {
            CurrentValue -= Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }

    public override void Stop() {
        CurrentValue = 0f;
        StopAllCoroutines();
        _valueTextUI.text = MaxValue.ToString("F1");
    }

    public void PlusClick() {
        MaxValue += ADDITION_VALUE;
    }

    public void MinusClick() {
        MaxValue -= ADDITION_VALUE;
    }

    public void ActivateButtons() {
        _plusButtonUI.interactable = true;
        _minusButtonUI.interactable = true;
    }

    public void DisableButtons() {
        _plusButtonUI.interactable = false;
        _minusButtonUI.interactable = false;
    }
}
