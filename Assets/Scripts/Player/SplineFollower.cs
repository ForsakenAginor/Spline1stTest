using SplineMesh;
using UnityEngine;

public class SplineFollower : MonoBehaviour
{
    [SerializeField] private Spline _spline;
    [SerializeField] private float _speed;
    [SerializeField] private float _sensitivity;

    private float _splineRate = 0f;
    private float _input = 0f;
    private float _lastMousePosition;
    private PlayerInput _playerInput;

    private void Start()
    {
        _lastMousePosition = Input.mousePosition.x;
        _playerInput = new PlayerInput();
    }

    private void Update()
    {
        _input += (_playerInput.GetX() - _lastMousePosition) * _sensitivity;
        _lastMousePosition = _playerInput.GetX();
        _input = Mathf.Clamp(_input, -1f, 1f);

        _splineRate += _speed * Time.deltaTime;

        if (_splineRate <= _spline.nodes.Count - 1)
            Place();
    }

    private void Place()
    {
        CurveSample sample = _spline.GetSample(_splineRate);

        Vector3 direction = Quaternion.AngleAxis(90, sample.up) * sample.tangent;
        transform.localPosition = sample.location + direction * _input;
        transform.localRotation = sample.Rotation;
    }
}