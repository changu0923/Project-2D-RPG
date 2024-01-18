using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MeteorHit : MonoBehaviour
{ 
    Player player;
    AudioSource audioSource;
    private void Awake()
    {
        player=FindObjectOfType<Player>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.PlayOneShot(GameManager.Instance.soundManager.meteorHit);
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
