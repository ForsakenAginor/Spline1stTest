using System;
using UnityEngine;

public class WalletPopManager : MonoBehaviour
{
    [SerializeField] private WalletPopUpView _popUp;
    [SerializeField] private WalletPopUpView _popDown;

    private Pickuper _pickuper;

    private void OnDestroy()
    {
        _pickuper.PickUped -= OnPickUped;
    }

    public void Init(Pickuper pickuper)
    {
        _pickuper = pickuper != null ? pickuper : throw new ArgumentNullException(nameof(pickuper));
        _pickuper.PickUped += OnPickUped;
        _popUp.Init(new Vector3(0, 0.5f, 0));
        _popDown.Init(new Vector3(0, -0.5f, 0));
    }

    private void OnPickUped(int value)
    {
        if (value < 0)
            _popDown.PlayAnimation(value);
        else
            _popUp.PlayAnimation(value);
    }
}
