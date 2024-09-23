using System;
using UnityEngine;

public class Pickuper : MonoBehaviour
{
    public event Action<int> PickUped;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IObtainable thing))
            PickUped?.Invoke(thing.PickUp());
    }
}
