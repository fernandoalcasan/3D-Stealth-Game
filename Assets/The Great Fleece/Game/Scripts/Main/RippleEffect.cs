using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleEffect : MonoBehaviour
{
    private void DestroyAfterEffect()
    {
        Destroy(gameObject);
    }
}
