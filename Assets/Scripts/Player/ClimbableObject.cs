using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbableObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")==true)
        {
            //print($"{collision.name}이 사다리 Enter");
            collision.GetComponent<Player>().isClimbAble = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {   
            //print($"{collision.name}이 사다리 Exit");
            collision.GetComponent<Player>().isClimbAble = false;
        }
    }
}
