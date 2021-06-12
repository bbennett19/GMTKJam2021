using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _patrolWaypoints;
    [SerializeField]
    private float _patrolSpeed;
    [SerializeField]
    private float _chaseSpeed;
    [SerializeField]
    private float _chaseRadius;

    private NavMeshAgent _navMeshAgent;
    private int _nextWaypoint = 0;
    private bool _chasing = false;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _patrolSpeed;
        GotoNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_chasing && PlayerInChaseRadius() && PlayerInLineOfSight())
        {
            Debug.Log("Start chasing");
            _navMeshAgent.speed = _chaseSpeed;
            _chasing = true;
        } else if (_chasing && !PlayerInChaseRadius())
        {
            Debug.Log("Stop chasing");
            _navMeshAgent.speed = _patrolSpeed;
            _chasing = false;
            GotoClosestWaypoint();
        }

        if (_chasing)
        {
            _navMeshAgent.destination = PlayerManager.Instance.GetCurrentPlayer().transform.position;
        }
        else if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.3f)
        {
            GotoNextWaypoint();
        }
    }

    private bool PlayerInChaseRadius()
    {
        return (transform.position - PlayerManager.Instance.GetCurrentPlayer().transform.position).magnitude < _chaseRadius;
    }

    private bool PlayerInLineOfSight()
    {
        Vector3 raycastDirection = PlayerManager.Instance.GetCurrentPlayer().transform.position - transform.position;
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, raycastDirection, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    private void GotoNextWaypoint()
    {
        if (_patrolWaypoints.Count == 0)
            return;

        _navMeshAgent.destination = _patrolWaypoints[_nextWaypoint].position;
        _nextWaypoint = (_nextWaypoint + 1) % _patrolWaypoints.Count;
    }

    private void GotoClosestWaypoint()
    {
        if (_patrolWaypoints.Count == 0)
            return;

        // This doesn't caluclate the actual closest waypoint but should be good enough
        int closestWaypointIndex = 0;
        float closestDistance = (transform.position - _patrolWaypoints[0].position).sqrMagnitude;

        for (int i = 1; i < _patrolWaypoints.Count; i++)
        {
            float distance = (transform.position - _patrolWaypoints[i].position).sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestWaypointIndex = i;
            }
        }

        _nextWaypoint = closestWaypointIndex;
        GotoNextWaypoint();
    }
}
