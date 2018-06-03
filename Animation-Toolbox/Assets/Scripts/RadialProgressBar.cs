using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgressBar : MonoBehaviour {

    public Transform loadingBar;

    public float maxAmount;

    [SerializeField]
    private float _currentAmount;
    public float CurrentAmount {
        get {
            return _currentAmount;
        }
        set {
            _currentAmount = value;
            loadingBar.GetComponent<Image>().fillAmount = _currentAmount / maxAmount;
        }
    }

    public void SetMaxAmount(float amount) {
        maxAmount = amount;
    }

    public void ResetAmount() {
        maxAmount = 1f;
        CurrentAmount = 0f;
    }

}
