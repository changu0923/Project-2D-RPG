using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorHit : MonoBehaviour
{ 
    public void Hit()
    {            
        Enemy target = gameObject.GetComponentInParent<Enemy>();        
        float damage = 30000f;
        int minDamage = (int)(damage - (damage * 0.15));
        int maxDamage = (int)(damage + (damage * 0.15));

        int rndDamage = Random.Range(minDamage, maxDamage);
        target.TakeDamage(rndDamage);
    }
    private void EndAnimation()
    {
        Destroy(gameObject);
    }
}
