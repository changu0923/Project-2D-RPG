using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireArrow : MonoBehaviour
{
    public float damage;
    public float speed = 0.5f;
    Rigidbody2D rb;
    int hitLimits = 3;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") == true)
        {
            if(hitLimits != 0) 
            {
                // 데미지 주기
            }
            else
            {
                Destroy(gameObject);
            }
        }        
    }


}
