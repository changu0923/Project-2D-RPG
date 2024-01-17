using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicClawStart : MonoBehaviour
{
    Player player;
    public GameObject MagicClawPrefab;
    public GameObject thisPrefab;
    SpriteRenderer spriteRenderer;
    

    private void Start()
    {
        player = GetComponentInParent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (player != null)
        {
            spriteRenderer.flipX = player.isFacingRight;
        }
        else
        {
            print("Player is Null");
        }
    }
    void Use()
    {
        Vector2 direction = player.isFacingRight ? Vector2.right : Vector2.left;
        Vector2 origin = player.currentAttackPoint.transform.position;
        float distance = 5f;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance);
        player.SetAttackMotion(Player.AttackMotion.SWING);
        if (hit.collider != null && hit.collider.tag == "Enemy")
        { 
            Transform target = hit.collider.GetComponent<Transform>();
            if (target != null)
            {
                GameObject magicClawHit = Instantiate(MagicClawPrefab, target.position, Quaternion.identity);
                magicClawHit.transform.parent = target.transform;
            }
        }
        else
        {
            print($"MagicClaw : {hit.collider.name}");
        }

    }

    private void AnimationEnd()
    {
        player.SetState(Player.State.IDLE);
        Destroy(thisPrefab);        
    }
}
