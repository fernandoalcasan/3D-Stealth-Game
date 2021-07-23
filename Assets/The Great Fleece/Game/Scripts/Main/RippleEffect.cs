using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleEffect : MonoBehaviour
{
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
    }
    private void DestroyAfterEffect()
    {
        Destroy(gameObject);
    }
}
