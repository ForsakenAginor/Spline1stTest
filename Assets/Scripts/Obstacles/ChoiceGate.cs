using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ChoiceGate : MonoBehaviour, IObtainable
{
    [SerializeField] private int _value;
    [SerializeField] private ChoiceGate _anotherVariant;

    public int PickUp()
    {
        gameObject.SetActive(false);
        _anotherVariant.gameObject.SetActive(false);
        return _value;
    }
}
