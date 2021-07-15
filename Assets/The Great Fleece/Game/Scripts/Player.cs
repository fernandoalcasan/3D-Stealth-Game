using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Player : MonoBehaviour
{
    public static Action<Vector3> OnCoinDistraction;

    [SerializeField]
    private GameObject _coinPrefab;
    [SerializeField]
    private AudioClip _coinAudio;

    private NavMeshAgent _navAgent;
    private Animator _anim;

    [SerializeField]
    private int _coins;
    private int _floorLayer;

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        if (_navAgent is null)
            Debug.LogError("NavMeshAgent is NULL");

        _anim = GetComponentInChildren<Animator>();
        if (_anim is null)
            Debug.LogError("Animator in children is NULL");

        _floorLayer = 1 << 7;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _floorLayer))
            {
                _navAgent.SetDestination(hitInfo.point);
            }
        }

        if (_coins > 0 && Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _floorLayer))
            {
                //To avoid collider issues
                hitInfo.point = new Vector3(hitInfo.point.x, -2f, hitInfo.point.z);
                ThrowCoin(hitInfo.point);
                _coins--;
            }
        }

        if (_navAgent.velocity.sqrMagnitude < 1f)
            _anim.SetBool("Walk", false);
        else
            _anim.SetBool("Walk", true);
    }

    private void ThrowCoin(Vector3 pos)
    {
        _anim.SetTrigger("Throw");
        StartCoroutine(CreateCoin(pos));
    }

    private IEnumerator CreateCoin(Vector3 pos)
    {
        yield return new WaitForSeconds(.5f);
        if (!(OnCoinDistraction is null))
            OnCoinDistraction(pos);
        Instantiate(_coinPrefab, pos, Quaternion.identity);
        AudioSource.PlayClipAtPoint(_coinAudio, pos);
    }

    private void OnDestroy()
    {
        OnCoinDistraction = null;
    }
}
