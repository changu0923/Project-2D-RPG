using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBronzeCoin : Item
{
    CircleCollider2D circleCollider;
    Rigidbody2D rb;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.AddForce(Vector3.up * 4f, ForceMode2D.Impulse);
    }   

    public override void Use()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy" || collision.collider.tag == "Player" || collision.collider.tag == "Dead" || collision.collider.tag == "Item") 
        {
            Physics2D.IgnoreCollision(circleCollider, collision.collider, true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy" || collision.collider.tag == "Player" || collision.collider.tag == "Dead" || collision.collider.tag == "Item")
        {
            Physics2D.IgnoreCollision(circleCollider, collision.collider, false);
        }
    }

}
