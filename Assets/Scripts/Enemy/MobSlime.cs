using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSlime : Enemy
{
    public enum SlimeState
    {
        IDLE    = 0,
        MOVE    = 1,
        HIT     = 2,
        DIE     = 3,
    }

    public float moveSpeed = 1f;

    float behaviorTime;
    DamagePopup popup;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Rigidbody2D rb;
    private float lifeTime = 1f;
    SlimeState state;

    private void Awake()
    {
        popup = GetComponent<DamagePopup>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        maxHP = 300000;
        currentHP = maxHP;
        state = SlimeState.IDLE;
    }

    private void Update()
    {
        switch (state) 
        {
            case SlimeState.IDLE:

                break;
            case SlimeState.MOVE:

                break;
            case SlimeState.HIT:

                break;
            case SlimeState.DIE:
                break;
        }
        animator.SetInteger("SlimeState", (int)state);
    }

    public void SetState(SlimeState state)
    {
        this.state = state;
    }


    public override void TakeDamage(float damage)
    {
        popup.PrintDamage((int)damage);
        currentHP -= (int)damage;
        SetState(SlimeState.HIT);
        StartCoroutine("KnockBackCoroutine");
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    public void Die()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        gameObject.tag = "Dead";
        StartCoroutine("FadeOutAndDestroy");
    }

    IEnumerator FadeOutAndDestroy()
    {
        // 시작 시간
        float startTime = Time.time;

        while (Time.time < startTime + lifeTime)
        {
            float normalizedTime = (Time.time - startTime) / lifeTime;
            Color currentColor = spriteRenderer.color;
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f - normalizedTime);

            yield return null;
        }
        Destroy(gameObject, 1f);
    }

    IEnumerator KnockBackCoroutine()
    {
        GameObject target = GameObject.Find("Player");
        if (target != null)
        {
            Vector2 dir = ((Vector2)(target.transform.position - transform.position)).normalized * moveSpeed * 2;
            rb.velocity = -dir;
            yield return new WaitForSeconds(0.1f);
            rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(0.2f);
            SetState(SlimeState.IDLE);
            yield return null;
        }
    }
}