using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Gates : MonoBehaviour
{
    [SerializeField] private Transform _leftGate;
    [SerializeField] private Transform _rightGate;
    [SerializeField] private float _animationSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<SplineFollower>())
        {
            Vector3 leftDoorValue = new Vector3(0f, -90f, 0f);
            Vector3 rightDoorValue = new Vector3(0f, 90f, 0f);
            _leftGate.DOLocalRotate(leftDoorValue, _animationSpeed).SetEase(Ease.Linear);
            _rightGate.DOLocalRotate(rightDoorValue, _animationSpeed).SetEase(Ease.Linear);
        }
    }
}
