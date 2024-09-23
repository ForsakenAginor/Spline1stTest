using System;
using System.Collections.Generic;
using System.Linq;

public class StatusMonitor
{
    private readonly Wallet _wallet;
    private readonly IEnumerable<StatusData> _thresholds;
    private Status _currentStatus;

    public StatusMonitor(Wallet wallet, IEnumerable<StatusData> thresholds)
    {
        _wallet = wallet != null ? wallet : throw new ArgumentNullException(nameof(wallet));
        _thresholds = thresholds != null ? thresholds : throw new ArgumentNullException(nameof(thresholds));
        _thresholds = _thresholds.OrderBy(o => o.Status).ToList();
        OnWalletChanged();

        _wallet.CurrencyChanged += OnWalletChanged;
    }

    public event Action<Status> StatusChanged;

    ~StatusMonitor()
    {
        _wallet.CurrencyChanged -= OnWalletChanged;
    }

    public Status CurrentStatus => _currentStatus;

    private void OnWalletChanged()
    {
        Status status = Status.Hobo;

        foreach (var threshold in _thresholds)
            if (_wallet.Currency >= threshold.Threshold)
                status = threshold.Status;

        if (_currentStatus != status)
        {
            _currentStatus = status;
            StatusChanged?.Invoke(status);
        }
    }
}
