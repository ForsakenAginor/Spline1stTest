using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WalletSliderView : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _image;

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

        _slider.maxValue = _thresholds.First(o => o.Status == Status.Milionaire).Threshold;
        _slider.value = _wallet.Currency;
        Recolor();

        _wallet.CurrencyChanged += OnWalletChanged;
    }

    private void Recolor()
    {
        _image.color = _thresholds.First(o => o.Status == _monitor.CurrentStatus).Color;
    }

    private void OnWalletChanged()
    {
        _slider.value = _wallet.Currency;
        Recolor();
    }
}
