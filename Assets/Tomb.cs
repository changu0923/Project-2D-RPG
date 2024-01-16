using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tomb : MonoBehaviour
{
    BoxCollider2D boxCollider;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy" || collision.collider.tag == "Player" || collision.collider.tag == "Dead" || collision.collider.tag == "Item")
        {
            Physics2D.IgnoreCollision(boxCollider, collision.collider, true);
        }
    }
}
