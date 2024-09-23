using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SuitChanger : MonoBehaviour
{
    [SerializeField] private List<SuitData> _suits;

    private StatusMonitor _statusMonitor;
    private GameObject _currentSuit;

    private void OnDestroy()
    {
        _statusMonitor.StatusChanged -= OnStatusChanged;
    }

    public void Init(StatusMonitor monitor)
    {
        _statusMonitor = monitor != null ? monitor : throw new ArgumentNullException(nameof(monitor));
        _suits.ForEach(o => o.Suit.gameObject.SetActive(false));
        _currentSuit = _suits.First(o => o.Status == _statusMonitor.CurrentStatus).Suit.gameObject;
        _currentSuit.SetActive(true);
        _statusMonitor.StatusChanged += OnStatusChanged;
    }

    private void OnStatusChanged(Status status)
    {
        _currentSuit?.SetActive(false);
        _currentSuit = _suits.First(o => o.Status == status).Suit.gameObject;
        _currentSuit.SetActive(true);
    }

    [Serializable]
    private struct SuitData
    {
        public Suit Suit;
        public Status Status;
    }
}