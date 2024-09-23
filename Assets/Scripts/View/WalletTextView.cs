using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.Linq;

public class WalletTextView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textField;

    private Wallet _wallet;
    private IEnumerable<StatusData> _thresholds;
    private StatusMonitor _monitor;

    private void OnDestroy()
    {
        _wallet.CurrencyChanged -= OnWalletChanged;
    }

    public void Init(Wallet wallet, IEnumerable<StatusData> thresholds, StatusMonitor monitor)
    {
        _wallet = wallet != null ? wallet : throw new ArgumentNullException(nameof(wallet));
        _thresholds = thresholds != null ? thresholds : throw new ArgumentNullException(nameof(thresholds));
        _monitor = monitor != null ? monitor : throw new ArgumentNullException(nameof(monitor));

        RefreshText();
        _wallet.CurrencyChanged += OnWalletChanged;
    }

    private void RefreshText()
    {
        StatusData data = _thresholds.First(o => o.Status == _monitor.CurrentStatus);
        _textField.text = data.Status.ToString();
        _textField.color = data.Color;
    }

    private void OnWalletChanged()
    {
        RefreshText();
    }
}
