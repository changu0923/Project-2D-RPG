using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MagicClawHit : MonoBehaviour
{
    public GameObject mySelf;

    void Hit()
    {
        Enemy target = GetComponentInParent<Enemy>();
        float damage = 500f;
        int minDamage = (int)(damage - (damage * 0.15));
        int maxDamage = (int)(damage + (damage * 0.15));

        int rndDamage = Random.Range(minDamage, maxDamage);
        target.TakeDamage(rndDamage);
    }

    void AnimationEnd()
    {
        Destroy(mySelf);
    }
}
