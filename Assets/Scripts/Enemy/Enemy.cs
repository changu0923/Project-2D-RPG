using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;    
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int currentHP;
    public int maxHP;

    public abstract void TakeDamage(float damage);
}
