using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent _navAgent;
    private Animator _anim;

    void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        if (_navAgent is null)
            Debug.LogError("NavMeshAgent is NULL");

        _anim = GetComponentInChildren<Animator>();
        if (_anim is null)
            Debug.LogError("Animator in children is NULL");
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                _navAgent.SetDestination(hitInfo.point);
            }
        }

        if(_navAgent.velocity.sqrMagnitude < 1f)
            _anim.SetBool("Walk", false);
        else
            _anim.SetBool("Walk", true);

    }
}
