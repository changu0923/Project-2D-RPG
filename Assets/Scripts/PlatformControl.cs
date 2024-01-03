using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour
{    
    EdgeCollider2D edgeCollider;
    void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        edgeCollider.enabled = false;
    }    

    public void Active()
    {
        edgeCollider.enabled = true;
    }

    public void JumpDown()
    {
        edgeCollider.enabled=false;
        Invoke("Active", .5f);
    }
}
