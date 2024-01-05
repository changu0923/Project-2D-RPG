using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    DamagePopup popup;

    public int currentHP;
    public int maxHP;

    private void Awake()
    {
        popup = GetComponent<DamagePopup>();
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
        Destroy(gameObject);
    }
}
