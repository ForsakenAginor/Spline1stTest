using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class Pickapable : MonoBehaviour, IObtainable
{
    [SerializeField] private int _value;

    public int PickUp()
    {
        gameObject.SetActive(false);
        return _value;
    }
}