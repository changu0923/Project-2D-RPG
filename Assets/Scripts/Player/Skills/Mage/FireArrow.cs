using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireArrow : MonoBehaviour
{
    public float damage;
    public float speed = 0.5f;
    public GameObject hitPrefab;
    public GameObject effectPrefab;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    int hitLimits = 3;
    Player player;
    bool isArrowFacingRight;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        isArrowFacingRight = player.isFacingRight;
        GameObject shotEffect = Instantiate(effectPrefab, player.currentAttackPoint.position, Quaternion.identity);
        shotEffect.GetComponentInChildren<SpriteRenderer>().flipX = isArrowFacingRight ? true : false;
        Destroy(shotEffect, 0.5f);
    }

    private void Update()
    {
        if (isArrowFacingRight == true)
        {
            spriteRenderer.flipX = false;
            rb.velocity = transform.right * speed;
        }
        else
        {
            spriteRenderer.flipX = true;
            rb.velocity = -transform.right * speed;
        }
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
