using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapScrolling : MonoBehaviour {

    [Range(1, 50)]
    [Header("Controllers")]
    public int panelCount;

    [Range(0, 500)]
    public int panelOffset;

    [Range(0f, 20f)]
    public float snapSpeed;

    [Range(0f, 5f)]
    public float scaleOffset;

    [Range(1f, 20f)]
    public float scaleSpeed;

    [Header("Other Objects")]
    public GameObject panelPrefab;
    public ScrollRect scrollRect;

    private GameObject[] _panelInstances;
    private Vector2[] _panelPositions;
    private Vector2[] _panelScales;

    private RectTransform _contentRect;
    private Vector2 _contentVector;

    private int _selectedPanelID;
    private bool _isScrolling;

    private void Start() {
        _contentRect = GetComponent<RectTransform>();

        _panelInstances = new GameObject[panelCount];
        _panelPositions = new Vector2[panelCount];
        _panelScales = new Vector2[panelCount];

        for (int ii = 0; ii < panelCount; ii++) {
            _panelInstances[ii] = Instantiate(panelPrefab, transform, false);

            if (ii == 0) continue;

            _panelInstances[ii].transform.localPosition = new Vector2(
                _panelInstances[ii-1].transform.localPosition.x + panelPrefab.GetComponent<RectTransform>().sizeDelta.x + (panelOffset * 3f), 
                _panelInstances[ii].transform.localPosition.y);

            _panelPositions[ii] = -_panelInstances[ii].transform.localPosition;
        }
    }

    private void FixedUpdate() {
        if (_contentRect.anchoredPosition.x <= _panelPositions[0].x && !_isScrolling || _contentRect.anchoredPosition.x <= _panelPositions[_panelPositions.Length-1].x && !_isScrolling)
            scrollRect.inertia = false;

        float nearestPos = float.MaxValue;
        for (int ii = 0; ii < panelCount; ii++) {
            float distance = Mathf.Abs(_contentRect.anchoredPosition.x - _panelPositions[ii].x);

            if (distance < nearestPos) {
                nearestPos = distance;
                _selectedPanelID = ii;
            }

            float scale = Mathf.Clamp(1 / (distance / panelOffset) * scaleOffset, 0.5f, 1f);
            _panelScales[ii].x = Mathf.SmoothStep(_panelInstances[ii].transform.localScale.x, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
            _panelScales[ii].y = Mathf.SmoothStep(_panelInstances[ii].transform.localScale.y, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
            _panelInstances[ii].transform.localScale = _panelScales[ii];
        }

        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrollVelocity < 400 && !_isScrolling) scrollRect.inertia = false;
        if (_isScrolling || scrollVelocity > 400) return;

        _contentVector.x = Mathf.SmoothStep(_contentRect.anchoredPosition.x, _panelPositions[_selectedPanelID].x, snapSpeed * Time.fixedDeltaTime);
        _contentRect.anchoredPosition = _contentVector;
    }

    public void Scrolling(bool scroll) {
        _isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }
}
