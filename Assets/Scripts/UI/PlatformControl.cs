using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour
{    
    EdgeCollider2D edgeCollider;
    Player player;
    float currentPosY;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        edgeCollider=GetComponent<EdgeCollider2D>();
    }


    private void Update()
    {
        CheckPlayerDown();
    }

    void CheckPlayerDown()
    {
        currentPosY = transform.position.y - player.transform.position.y;
        if (currentPosY > 0) 
        {
            edgeCollider.enabled = false;
        }
        else if(currentPosY < 0.2)
        {
            edgeCollider.enabled = true;
        }
    }
}
