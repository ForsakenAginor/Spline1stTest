using UnityEngine;

public class Root : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Pickuper _player;
    [SerializeField] private int _startCurrency;

    [Header("View")]
    [SerializeField] private WalletSliderView _sliderView;
    [SerializeField] private WalletTextView _textView;
    [SerializeField] private StatusViewPopUp _popUpStatus;
    [SerializeField] private SuitChanger _suitChanger;
    [SerializeField] private WalletPopUpView _popUpView;

    [Header("Other")]
    [SerializeField] private StatusThresholds _statusThresholds;

    private Wallet _wallet;
    private SplineFollower _splineFollower;
    private StatusMonitor _statusMonitor;

    private void Start()
    {
        _splineFollower = _player.GetComponent<SplineFollower>();
        _wallet = new(_startCurrency);
        _statusMonitor = new StatusMonitor(_wallet, _statusThresholds.Thresholds);
        _sliderView.Init(_wallet, _statusThresholds.Thresholds, _statusMonitor);
        _textView.Init(_wallet, _statusThresholds.Thresholds, _statusMonitor);
        _popUpStatus.Init(_statusThresholds.Thresholds, _statusMonitor);
        _suitChanger.Init(_statusMonitor);
        _popUpView.Init(_player);

        _player.PickUped += OnPickUped;
        _wallet.Broke += OnBroke;
    }

    private void OnDestroy()
    {
        _player.PickUped -= OnPickUped;
        _wallet.Broke -= OnBroke;
    }

    private void OnBroke()
    {
        _splineFollower.enabled = false;
    }

    private void OnPickUped(int thingValue)
    {
        if (thingValue < 0)
            _wallet.SpentCurrency(thingValue);
        else
            _wallet.AddCurrency(thingValue);
    }
}
