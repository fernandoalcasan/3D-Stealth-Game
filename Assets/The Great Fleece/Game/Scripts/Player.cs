using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent _navAgent;

    void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        if (_navAgent is null)
            Debug.LogError("NavMeshAgent is NULL");
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
            {
                _navAgent.SetDestination(hitInfo.point);
            }
        }
    }
}
