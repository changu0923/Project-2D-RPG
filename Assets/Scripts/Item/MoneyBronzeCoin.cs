using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBronzeCoin : Item
{
    CircleCollider2D circleCollider;
    Rigidbody2D rb;
    Player player;
    int value = 10;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        rb.AddForce(Vector3.up * 4f, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if(player!=null)
        {
           if( Mathf.Abs( player.transform.position.x - gameObject.transform.position.x) < 0.15f )
            {
                Use();                
            }
        }
    }

    public override void Use()
    {
        player.EarnMoney(value);
        Destroy(gameObject);
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
