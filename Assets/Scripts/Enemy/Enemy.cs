using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;    
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public string mobName;
    public int currentHP;
    public int maxHP;
    public float damage;
    public int exp;

    public GameObject[] dropableItems;
    public GameObject[] moneyItems;

    public abstract void TakeDamage(float damage); 
}
