using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int currentHP;
    public int maxHP;

    void Start()
    {
        maxHP = 100;
        currentHP = maxHP;
    }
   
    public void TakeDamage(float damage)
    {
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
