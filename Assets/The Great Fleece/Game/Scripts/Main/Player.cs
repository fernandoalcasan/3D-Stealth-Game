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
    private GameObject _navRipplePrefab;
    [SerializeField]
    private GameObject _coinRipplePrefab;
    [SerializeField]
    private AudioClip[] _audios;

    private NavMeshAgent _navAgent;
    private Animator _anim;

    [SerializeField]
    private int _coins;
    private int _floorLayer;

    public enum FloorType
    {
        Marble,
        Soft
    }
    private FloorType _floorType;

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        if (_navAgent is null)
            Debug.LogError("NavMeshAgent is NULL");

        _anim = GetComponentInChildren<Animator>();
        if (_anim is null)
            Debug.LogError("Animator in children is NULL");

        _floorLayer = 1 << 7;
        _floorType = FloorType.Soft;

        if(_audios.Length < 3)
        {
            Debug.LogError("Please set the respective SFX audios");
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _floorLayer))
            {
                _navAgent.SetDestination(hitInfo.point);
                Instantiate(_navRipplePrefab, hitInfo.point, _navRipplePrefab.transform.rotation);
            }
        }

        if (_coins > 0 && Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _floorLayer))
            {
                Instantiate(_coinRipplePrefab, hitInfo.point, _coinRipplePrefab.transform.rotation);
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
        AudioManager.Instance.PlaySFX(_audios[0], 0.4f);
    }

    private void PlayFootstep()
    {
        switch (_floorType)
        {
            case FloorType.Marble:
                AudioManager.Instance.PlaySFX(_audios[2], 0.4f);
                break;
            case FloorType.Soft:
                AudioManager.Instance.PlaySFX(_audios[1], 0.4f);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Marble"))
            _floorType = FloorType.Marble;
        else if (other.CompareTag("Soft"))
            _floorType = FloorType.Soft;
    }

    private void OnDestroy()
    {
        OnCoinDistraction = null;
    }
}
