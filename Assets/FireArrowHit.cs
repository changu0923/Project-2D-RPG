using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrowHit : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, .6f);
    }
}
