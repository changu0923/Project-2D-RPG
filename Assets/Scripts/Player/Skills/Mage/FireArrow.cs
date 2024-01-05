using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireArrow : MonoBehaviour
{
    public float damage;
    public float speed = 0.5f;
    public GameObject hitPrefab;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    int hitLimits = 3;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.CompareTag("Enemy") == true)
        {
            int minDamage = (int)(damage - (damage * 0.1));
            int maxDamage = (int)(damage + (damage * 0.1));

            int rndDamage = Random.Range(minDamage, maxDamage);

            if (hitLimits != 0) 
            {
                // 데미지 주기
                Enemy enemy = collision.GetComponent<Enemy>();
                Instantiate(hitPrefab, enemy.transform.localPosition, enemy.transform.localRotation);   
                enemy.TakeDamage(rndDamage);
                hitLimits--;
            }
            else
            {
                boxCollider.enabled = false;
            }
        }        
    }


}
