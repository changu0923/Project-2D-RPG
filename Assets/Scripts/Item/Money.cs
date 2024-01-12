using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : Item
{
    public GameObject bronzeCoinPrefab;
    public GameObject goldCoinPrefab;
    public GameObject billPrefab;
    public GameObject moneyPocketPrefab;

    Player player;
    CircleCollider2D circleCollider;
    Rigidbody2D rb;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    public override void Use()
    {
        
    }
    public void SpawnMoney(int amount, Transform target)
    {
        GameObject dropMoney;
        if( amount>0 && amount<50 )
        {
            dropMoney = bronzeCoinPrefab;
        }
        else if( amount>=50 && amount <100 )
        {
            dropMoney = goldCoinPrefab;
        }
        else if( amount >=100 && amount <1000 )
        {
            dropMoney= billPrefab;
        }
        else if( amount >= 1000)
        {
            dropMoney= moneyPocketPrefab;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Platform"))
        {
            rb.gravityScale = 0;
            circleCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
        }
    }

}