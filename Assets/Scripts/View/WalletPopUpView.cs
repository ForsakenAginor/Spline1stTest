using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WalletPopUpView : MonoBehaviour
{
    [SerializeField] private Transform _holder;
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private Image _icon;
    [SerializeField] private float _animationDuration;
    [SerializeField] private float _disappearDuration;

    private Pickuper _pickuper;
    private int _buffer;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Tween _movingTween;
    private Tween _dissapearImageTween;
    private Tween _dissapearTextTween;

    private void Start()
    {
        _holder.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _pickuper.PickUped -= OnPickUped;        
    }

    public void Init(Pickuper pickuper)
    {
        _pickuper = pickuper != null ? pickuper : throw new ArgumentNullException(nameof(pickuper));
        _startPosition = _holder.localPosition;
        _endPosition = _startPosition + new Vector3(0, 0.5f, 0);
        _pickuper.PickUped += OnPickUped;
    }

    private void OnPickUped(int value)
    {
        _holder.gameObject.SetActive(true);
        _movingTween?.Kill();
        _dissapearImageTween?.Kill();
        _dissapearTextTween?.Kill();
        _holder.localPosition = _startPosition;
        BecameVisible(_icon);
        BecameVisible(_textField);

        _buffer += value;
        _textField.text = $"+{_buffer}";
        _movingTween = _holder.DOLocalMove(_endPosition, _animationDuration).OnComplete(RestoreToDefault);
        _dissapearImageTween = _icon.DOFade(0, _disappearDuration);
        _dissapearTextTween = _textField.DOFade(0, _disappearDuration);
    }

    private void RestoreToDefault()
    {
        _buffer = 0;
        _holder.gameObject.SetActive(false);
    }

    private void BecameVisible(MaskableGraphic image)
    {
        var tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;
    }
}