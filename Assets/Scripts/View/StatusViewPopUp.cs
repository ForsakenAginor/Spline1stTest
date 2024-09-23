using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StatusViewPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private float _effectSpeed;
    [SerializeField] private float _effectDuration;

    private IEnumerable<StatusData> _thresholds;
    private StatusMonitor _monitor;
    private Coroutine _currentCoroutine;
    private Tween _currentTween;

    private void Start()
    {
        _textField.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _monitor.StatusChanged -= OnStatusChanged;
    }

    public void Init(IEnumerable<StatusData> thresholds, StatusMonitor monitor)
    {
        _thresholds = thresholds != null ? thresholds : throw new ArgumentNullException(nameof(thresholds));
        _monitor = monitor != null ? monitor : throw new ArgumentNullException(nameof(monitor));

        _monitor.StatusChanged += OnStatusChanged;
    }

    private void OnStatusChanged(Status status)
    {
        if (_currentCoroutine != null)
            StopCoroutine(ShowLabel());

        if(_currentTween != null)
            _currentTween.Kill();

        _textField.gameObject.SetActive(false);
        _textField.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        _textField.text = status.ToString();
        _textField.color = _thresholds.First(o => o.Status == status).Color;
        _textField.gameObject.SetActive(true);
        _currentTween =_textField.transform.DOScale(Vector3.one, _effectSpeed);
        _currentCoroutine = StartCoroutine(ShowLabel());
    }

    private IEnumerator ShowLabel()
    {
        WaitForSeconds delay = new WaitForSeconds(_effectSpeed + _effectDuration);
        yield return delay;
        _textField.gameObject.SetActive(false);
    }
}
