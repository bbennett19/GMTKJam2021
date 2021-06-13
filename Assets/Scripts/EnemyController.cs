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
    [SerializeField]
    private float _attackDistance;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _startChasingClip;
    [SerializeField]
    private AudioClip _chasingClip;
    [SerializeField]
    private AudioClip _attackClip;
    [SerializeField]
    private AudioClip _patrolClip;

    private NavMeshAgent _navMeshAgent;
    private int _nextWaypoint = 0;
    private bool _chasing = false;
    private bool _done = false;
    private Coroutine _audioCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _patrolSpeed;
        GotoNextWaypoint();
        PlayClip(_patrolClip, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_done)
        {
            if (!_chasing && PlayerInChaseRadius() && PlayerInLineOfSight())
            {
                Debug.Log("Start chasing");
                _navMeshAgent.speed = _chaseSpeed;
                _chasing = true;
                PlayShortAndLong(_startChasingClip, _chasingClip);
            }
            else if (_chasing && !PlayerInChaseRadius())
            {
                Debug.Log("Stop chasing");
                PlayClip(_patrolClip, true);
                _navMeshAgent.speed = _patrolSpeed;
                _chasing = false;
                GotoClosestWaypoint();
            }
            else if (_chasing && PlayerInAttackRadius())
            {
                Debug.Log("Attack");
                PlayClip(_attackClip, false);
                PlayerController player = PlayerManager.Instance.GetClosestPlayer(transform.position).GetComponent<PlayerController>();
                player.SetDead();
                PlayerManager.Instance.DisableSwap(true);
                GameOverCanvas.Instance.TriggerGameOver(player);
                _navMeshAgent.isStopped = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _done = true;
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
    }

    private bool PlayerInChaseRadius()
    {
        GameObject player = PlayerManager.Instance.GetClosestPlayer(transform.position);

        return player != null && (transform.position - player.transform.position).magnitude < _chaseRadius;
    }

    private bool PlayerInAttackRadius()
    {
        GameObject player = PlayerManager.Instance.GetClosestPlayer(transform.position);

        return player != null && (transform.position - player.transform.position).magnitude < _attackDistance;
    }

    private bool PlayerInLineOfSight()
    {
        GameObject player = PlayerManager.Instance.GetClosestPlayer(transform.position);

        if (player)
        {
            Vector3 raycastDirection = player.transform.position - transform.position;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, raycastDirection, out hit) && Vector3.Dot(transform.forward, raycastDirection.normalized) > 0)
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    return true;
                }
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

    private void PlayShortAndLong(AudioClip shortClip, AudioClip longClip)
    {
        if (_audioCoroutine != null)
        {
            StopCoroutine(_audioCoroutine);
        }

        _audioCoroutine = StartCoroutine(PlayTwoClips(shortClip, longClip));
    }

    public void PlayClip(AudioClip clip, bool looping)
    {
        if (_audioCoroutine != null)
        {
            StopCoroutine(_audioCoroutine);
            _audioCoroutine = null;
        }

        _audioSource.clip = clip;
        _audioSource.loop = looping;
        _audioSource.Play();
    }

    IEnumerator PlayTwoClips(AudioClip shortClip, AudioClip longClip)
    {
        PlayClip(shortClip, false);

        while (_audioSource.isPlaying)
        {
            yield return null;
        }

        PlayClip(longClip, true);
    }
}
