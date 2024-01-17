using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorHit : MonoBehaviour
{ 
    Player player;
    private void Awake()
    {
        player=FindObjectOfType<Player>();
    }
    public void Hit()
    {            
        Enemy target = gameObject.GetComponentInParent<Enemy>();
        float damage = (player.level * 570f) + ((player.level * 570f) * 0.25f);
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
