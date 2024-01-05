using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    DamagePopup popup;
    SpriteRenderer spriteRenderer;

    public int currentHP;
    public int maxHP;
    private float lifeTime = 1f;

    private void Awake()
    {
        popup = GetComponent<DamagePopup>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        currentHP = maxHP;
    }
   
    public void TakeDamage(float damage)
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
