using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _patrolWaypoints;
    private NavMeshAgent _navMeshAgent;
    private int _nextWaypoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        GotoNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.3f)
        {
            GotoNextWaypoint();
        }
    }

    private void GotoNextWaypoint()
    {
        if (_patrolWaypoints.Count == 0)
            return;

        _navMeshAgent.destination = _patrolWaypoints[_nextWaypoint].position;
        _nextWaypoint = (_nextWaypoint + 1) % _patrolWaypoints.Count;
    }
}
