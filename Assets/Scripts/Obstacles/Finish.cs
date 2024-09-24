using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Finish : MonoBehaviour
{
    public Action Finished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SplineFollower _))
            Finished?.Invoke();
    }

    public void Init(Action callback)
    {
        Finished = callback != null ? callback : throw new ArgumentNullException(nameof(callback));
    }
}