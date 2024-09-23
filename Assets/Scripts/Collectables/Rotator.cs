using UnityEngine;
using DG.Tweening;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private void Start()
    {
        int infinity = -1;
        Vector3 target = new (0, 180, 0);

        transform.DOLocalRotate(target, _rotationSpeed)
            .SetLoops(infinity, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }
}
