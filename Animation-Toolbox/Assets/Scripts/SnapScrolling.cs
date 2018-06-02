using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapScrolling : MonoBehaviour {

    [Header("Controllers")]
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

    private List<GameObject> _panelInstances = new List<GameObject>();
    private List<Vector2> _panelPositions = new List<Vector2>();
    private List<Vector2> _panelScales = new List<Vector2>();

    private RectTransform _contentRect;
    private Vector2 _contentVector;

    private int _selectedPanelID;
    private bool _isScrolling;

    private void Awake() {
        _contentRect = GetComponent<RectTransform>();
    }

    public GameObject CreateAPanel() {
        GameObject newPanel = Instantiate(panelPrefab, transform, false);

        Vector2 newPanelPosition = GetNewPanelPosition(newPanel);
        newPanel.transform.localPosition = newPanelPosition;

        _panelScales.Add(Vector2.one);
        _panelPositions.Add(-newPanelPosition);
        _panelInstances.Add(newPanel);

        return newPanel;
    }

    private Vector2 GetNewPanelPosition(GameObject newPanel) {
        if (_panelInstances.Count == 0) return Vector2.zero;

        int lastPanelIndex = _panelInstances.Count;

        return new Vector2(_panelInstances[lastPanelIndex - 1].transform.localPosition.x + panelPrefab.GetComponent<RectTransform>().sizeDelta.x + (panelOffset * 3f),
            newPanel.transform.localPosition.y);
    }

    private void FixedUpdate() {
        if (_contentRect.anchoredPosition.x <= _panelPositions[0].x && !_isScrolling || _contentRect.anchoredPosition.x <= _panelPositions[_panelPositions.Count-1].x && !_isScrolling)
            scrollRect.inertia = false;

        float nearestPos = float.MaxValue;
        for (int ii = 0; ii < _panelInstances.Count; ii++) {
            float distance = Mathf.Abs(_contentRect.anchoredPosition.x - _panelPositions[ii].x);

            if (distance < nearestPos) {
                nearestPos = distance;
                _selectedPanelID = ii;
            }

            float scale = Mathf.Clamp(1 / (distance / panelOffset) * scaleOffset, 0.5f, 1f);
            _panelScales[ii] = new Vector2(Mathf.SmoothStep(_panelInstances[ii].transform.localScale.x, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime),
                Mathf.SmoothStep(_panelInstances[ii].transform.localScale.y, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime));
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
