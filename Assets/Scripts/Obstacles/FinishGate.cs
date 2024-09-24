using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FinishGate : MonoBehaviour
{
    [SerializeField] private Transform _leftGate;
    [SerializeField] private Transform _rightGate;
    [SerializeField] private float _animationSpeed;
    [SerializeField] private Status _status;

    private StatusMonitor _statusMonitor;

    public Action Finished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SplineFollower _))
        {
            if ((int)_status > (int)_statusMonitor.CurrentStatus)
                Finished?.Invoke();
            else
            {
                Vector3 leftDoorValue = new Vector3(0f, -90f, 0f);
                Vector3 rightDoorValue = new Vector3(0f, 90f, 0f);
                _leftGate.DOLocalRotate(leftDoorValue, _animationSpeed).SetEase(Ease.Linear);
                _rightGate.DOLocalRotate(rightDoorValue, _animationSpeed).SetEase(Ease.Linear);
            }
        }
    }

    public void Init(StatusMonitor statusMonitor, Action callback)
    {
        _statusMonitor = statusMonitor != null ? statusMonitor : throw new ArgumentNullException(nameof(statusMonitor));
        Finished = callback != null ? callback : throw new ArgumentNullException(nameof(callback));
    }
}