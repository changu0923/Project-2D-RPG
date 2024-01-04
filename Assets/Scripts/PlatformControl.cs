using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour
{    
    EdgeCollider2D edgeCollider;
    PlatformEffector2D effector;
    void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();     
        effector = GetComponent<PlatformEffector2D>();
    }

    public void PlayerUp()
    {
        effector.rotationalOffset = 0;
    }

    public void PlayerDown()
    {
        effector.rotationalOffset = 180;
        StartCoroutine("PlatformDownCoolTimeCoroutine");
    }

    IEnumerator PlatformDownCoolTimeCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerUp();
        yield return null;
    }
}
