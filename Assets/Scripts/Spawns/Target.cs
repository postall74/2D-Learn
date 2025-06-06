using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private const float WaypointReachedThresholdSqr = 0.01f;

    [SerializeField] private List<Transform> _waypoints = new();
    [SerializeField] private float _speed = 5f;

    private int _currentWaypointIndex = 0;

    private void Update()
    {
        if (_waypoints.Count <= 0)
            return;

        Transform currentWaypoint = _waypoints[_currentWaypointIndex];
        transform.position = Vector3.MoveTowards
            (
                transform.position,
                currentWaypoint.position,
                _speed * Time.deltaTime
            );

        if ((transform.position - currentWaypoint.position).sqrMagnitude <  WaypointReachedThresholdSqr)
        {
            _currentWaypointIndex++;
            _currentWaypointIndex %= _waypoints.Count;
        }    
    }
}