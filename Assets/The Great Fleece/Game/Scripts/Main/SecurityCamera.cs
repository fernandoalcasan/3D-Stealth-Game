using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject _cutscene;
    private MeshRenderer _meshRenderer;
    private Animator _anim;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        if (_meshRenderer is null)
            Debug.LogError("Mesh Renderer is NULL");

        _anim = GetComponentInParent<Animator>();
        if (_anim is null)
            Debug.LogError("Animator in parent is NULL");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _meshRenderer.material.SetColor("_TintColor", new Color32(255, 10, 0, 10));
            _anim.enabled = false;
            StartCoroutine(TriggerCutscene());
        }
    }

    private IEnumerator TriggerCutscene()
    {
        yield return new WaitForSeconds(0.5f);
        _cutscene.SetActive(true);
    }
}
