using System;
using UnityEngine;

public class Pickuper : MonoBehaviour
{
    [SerializeField] private AudioSource _positive;
    [SerializeField] private AudioSource _negative;

    public event Action<int> PickUped;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IObtainable thing))
        {
            int value = thing.PickUp();
            PickUped?.Invoke(value);

            if(value > 0)
                _positive.Play();
            else
                _negative.Play();
        }
    }
}
