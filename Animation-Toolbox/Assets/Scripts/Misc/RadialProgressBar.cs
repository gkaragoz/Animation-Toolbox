using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
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

            if (maxAmount == 0)
                loadingBar.GetComponent<Image>().fillAmount = 0f;
            else
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
