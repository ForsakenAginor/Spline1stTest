using System;
using UnityEngine;

public class AnimatorManager
{
    private const string IsPoor = "IsPoor";
    private const string Spinning = "Spinning";
    private const string IsWon = "IsWon";
    private const string IsLose = "IsLose";
    private const string IsMoving = "IsMoving";

    private readonly Animator _animator;
    private readonly StatusMonitor _statusMonitor;
    private readonly Wallet _wallet;

    private Status _lastStatus;

    public AnimatorManager(Animator animator, StatusMonitor monitor, Wallet wallet)
    {
        _animator = animator != null ? animator : throw new ArgumentNullException(nameof(animator));
        _statusMonitor = monitor != null ? monitor : throw new ArgumentNullException(nameof(monitor));
        _wallet = wallet != null ? wallet : throw new ArgumentNullException(nameof(wallet));
        _lastStatus = _statusMonitor.CurrentStatus;
        _animator.SetBool(IsMoving, true);

        if (_lastStatus == Status.Hobo || _lastStatus == Status.Poor)
            _animator.SetBool(IsPoor, true);

        _statusMonitor.StatusChanged += OnStatusChanged;
        _wallet.Broke += OnBroke;
        _wallet = wallet;
    }

    ~AnimatorManager()
    {
        _statusMonitor.StatusChanged -= OnStatusChanged;
        _wallet.Broke -= OnBroke;
    }

    public void Celebrate()
    {
        _animator.SetBool(IsWon, true);
    }

    private void OnBroke()
    {
        _animator.SetBool(IsLose, true);
    }

    private void OnStatusChanged(Status status)
    {
        if (status == Status.Hobo || status == Status.Poor)
            _animator.SetBool(IsPoor, true);
        else
            _animator.SetBool(IsPoor, false);

        if ((int)_lastStatus < (int)status)
            _animator.SetTrigger(Spinning);

        _lastStatus = status;
    }
}
