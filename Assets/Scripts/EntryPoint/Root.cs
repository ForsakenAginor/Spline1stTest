using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Root : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Pickuper _player;
    [SerializeField] private int _startCurrency;
    [SerializeField] Animator _animator;

    [Header("View")]
    [SerializeField] private WalletSliderView _sliderView;
    [SerializeField] private WalletTextView _textView;
    [SerializeField] private StatusViewPopUp _popUpStatus;
    [SerializeField] private SuitChanger _suitChanger;
    [SerializeField] private WalletPopManager _popManager;

    [Header("Audio")]
    [SerializeField] private AudioSource _celebrateAudio;

    [Header("Other")]
    [SerializeField] private StatusThresholds _statusThresholds;
    [SerializeField] private FinishGate[] _gates;
    [SerializeField] private Finish _finish;

    [Header("UI")]
    [SerializeField] private GameObject _endGameCanvas;
    [SerializeField] private GameObject _winCanvas;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _nextLevelButton;

    private Wallet _wallet;
    private SplineFollower _splineFollower;
    private StatusMonitor _statusMonitor;
    private AnimatorManager _animatorManager;

    private void Start()
    {
        _splineFollower = _player.GetComponent<SplineFollower>();
        _wallet = new(_startCurrency);
        _statusMonitor = new StatusMonitor(_wallet, _statusThresholds.Thresholds);
        _sliderView.Init(_wallet, _statusThresholds.Thresholds, _statusMonitor);
        _textView.Init(_wallet, _statusThresholds.Thresholds, _statusMonitor);
        _popUpStatus.Init(_statusThresholds.Thresholds, _statusMonitor);
        _suitChanger.Init(_statusMonitor);
        _popManager.Init(_player);

        _player.PickUped += OnPickUped;
        _wallet.Broke += OnBroke;

        _animatorManager = new(_animator, _statusMonitor, _wallet);
        _gates.ToList().ForEach(gate => gate.Init(_statusMonitor, Celebrate));
        _finish.Init(Celebrate);

        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _nextLevelButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDestroy()
    {
        _player.PickUped -= OnPickUped;
        _wallet.Broke -= OnBroke;
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _nextLevelButton.onClick.RemoveListener(OnRestartButtonClick);
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(Scenes.FirstLevel.ToString());
    }

    private void Celebrate()
    {
        _splineFollower.enabled = false;
        _animatorManager.Celebrate();
        _celebrateAudio.Play();
        _winCanvas.SetActive(true);
    }

    private void OnBroke()
    {
        _splineFollower.enabled = false;
        _endGameCanvas.SetActive(true);
    }

    private void OnPickUped(int thingValue)
    {
        if (thingValue < 0)
            _wallet.SpentCurrency(-thingValue);
        else
            _wallet.AddCurrency(thingValue);
    }
}
