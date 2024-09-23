using System.Collections.Generic;
using UnityEngine;

public class StatusThresholds : MonoBehaviour
{
    [SerializeField] private StatusData[] _thresholds;

    public IEnumerable<StatusData> Thresholds => _thresholds;
}
