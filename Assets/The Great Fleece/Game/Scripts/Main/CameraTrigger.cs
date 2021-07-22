using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform _camera;
    private Camera _main;

    private void Start()
    {
        _main = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _main.transform.SetPositionAndRotation(_camera.position, _camera.rotation);
        }
    }
}
