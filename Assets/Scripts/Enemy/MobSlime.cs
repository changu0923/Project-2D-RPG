using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSlime : Enemy
{
    enum SlimeState
    {
        IDLE,
        MOVE,
        HIT,
        DIE,
    }

    float behaviorTime;

    DamagePopup popup;
    SpriteRenderer spriteRenderer;
    private float lifeTime = 1f;
    SlimeState state;

    private void Awake()
    {
        popup = GetComponent<DamagePopup>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        maxHP = 50;
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
    }


    public override void TakeDamage(float damage)
    {
        popup.PrintDamage((int)damage);
        currentHP -= (int)damage;

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
}