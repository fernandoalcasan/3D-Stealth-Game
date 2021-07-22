using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIGuard : MonoBehaviour
{
    private NavMeshAgent _navAgent;
    private Animator _anim;

    [SerializeField]
    private List<Transform> _waypoints;
    private int _target;

    [SerializeField]
    private GameObject _gameOverCutscene;

    private bool _distracted;

    void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        if (_navAgent is null)
            Debug.LogError("Nav Mesh Agent is NULL");

        _anim = GetComponent<Animator>();
        if (_anim is null)
            Debug.LogError("Animator is NULL");

        if (_waypoints.Count > 1 && !(_waypoints[0] is null))
            StartCoroutine(WaitToPatrol());
        else
            Debug.LogError("Please select 2 waypoints at least");

        Player.OnCoinDistraction += GetDistracted;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_distracted)
            return;

        if(other.CompareTag("Waypoint"))
        {
            _target++;

            if (_target == _waypoints.Count)
            {
                _waypoints.Reverse();
                _target = 1;
                StartCoroutine(WaitToPatrol());
            }
            else
                SetNextTarget();
        }
        else if(other.CompareTag("Player"))
        {
            _gameOverCutscene.SetActive(true);
        }
    }

    private void SetNextTarget()
    {
        if (!(_waypoints[_target] is null))
            _navAgent.SetDestination(_waypoints[_target].position);
        else
            Debug.LogError("Target waypoint is null");
    }

    private IEnumerator WaitToPatrol()
    {
        yield return new WaitForSeconds(1f);
        _anim.SetBool("Walk", false);
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        _anim.SetBool("Walk", true);
        SetNextTarget();
    }

    private void GetDistracted(Vector3 pos)
    {
        if(Vector3.Distance(transform.position, pos) <= 15f)
        {
            StopAllCoroutines();
            _distracted = true;
            _navAgent.SetDestination(pos);
            _anim.SetBool("Walk", true);
            StartCoroutine(FollowDistraction(pos));
        }
    }

    private IEnumerator FollowDistraction(Vector3 pos)
    {
        while(Vector3.Distance(transform.position, pos) > 2.5f)
            yield return null;

        _anim.SetBool("Walk", false);
        _navAgent.SetDestination(transform.position);
    }

    private void OnDestroy()
    {
        Player.OnCoinDistraction -= GetDistracted;
    }
}
