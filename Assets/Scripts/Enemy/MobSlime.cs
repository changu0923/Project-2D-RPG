using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class MobSlime : Enemy
{
    public enum SlimeState
    {
        IDLE    = 0,
        MOVE    = 1,
        HIT     = 2,
        DIE     = 3,
    }

    public float moveSpeed = 1f;

    float behaviorTime;
    float lifeTime = 1f;
    bool isHit;
    bool isMoveable;
    bool isDead;
    bool moveRight;

    DamagePopup popup;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Rigidbody2D rb;
    Transform currentMoveTarget;
    AudioSource audioSource;

    SlimeState state;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        popup = GetComponent<DamagePopup>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {       
        currentHP = maxHP;
        state = SlimeState.IDLE;
        isMoveable = true;
        int move = UnityEngine.Random.Range(0, 2);
        if(move == 0)
        {
            moveRight = false;
        }
        else
        {
            moveRight = true;
        }
    }

    private void Update()
    {
        switch (state) 
        {
            case SlimeState.IDLE:
                Idle();
                break;
            case SlimeState.MOVE:
                Move();
                break;
            case SlimeState.HIT:
                break;
            case SlimeState.DIE:
                animator.SetBool("isDead", true);
                break;
        }
        animator.SetInteger("SlimeState", (int)state);
    }

    void Idle()
    {
        isMoveable = true;
        rb.velocity = Vector2.zero;
        int RandomSeconds = UnityEngine.Random.Range(2, 5);
        StartCoroutine(ChooseAction(RandomSeconds));        
    }

    void Move()
    {
        if (isDead == false)
        {
            if (isHit == true)
            {
                // TODO : 플레이어방향으로 이동
                GameObject player = GameObject.Find("Player");
                Vector3 playerPosition = player.transform.position;
                Vector3 direction = (playerPosition - transform.position).normalized;
                rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
            }
            else
            {
                SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
                Transform movePointLeft = spawnManager.GetComponent<SpawnManager>().movePointLeft;
                Transform movePointRight = spawnManager.GetComponent<SpawnManager>().movePointRight;
                if (Mathf.Abs(transform.position.x - movePointLeft.position.x) < 0.25f)
                {
                    moveRight = true;
                    SetState(SlimeState.IDLE);
                }
                else if (Mathf.Abs(transform.position.x - movePointRight.position.x) < 0.25f)
                {
                    moveRight = false;
                    SetState(SlimeState.IDLE);
                }
                currentMoveTarget = moveRight ? movePointRight : movePointLeft;
                Vector3 direction = (currentMoveTarget.position - transform.position).normalized;
                rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

            }

            // 이동하는 방향에 따라 스프라이트 뒤집기
            if (rb.velocity.x < 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (rb.velocity.x > 0)
            {
                spriteRenderer.flipX = true;
            }
        }       
    }
   
   
    public void SetState(SlimeState state)
    {
        this.state = state;
    }


    public override void TakeDamage(float damage)
    {
        popup.PrintDamage((int)damage);
        audioSource.PlayOneShot(GameManager.Instance.soundManager.slimeDamage);
        if (isDead != true)
        {
            currentHP -= (int)damage;
            isHit = true;
            SetState(SlimeState.HIT);
            StartCoroutine("KnockBackCoroutine");
            if (currentHP <= 0)
            {
                currentHP = 0;
                Die();
            }
        }
    }

    public void Die()
    {
        isDead = true;
        GameObject player = GameObject.Find("Player");
        player.GetComponent<Player>().GainExp(exp);
        SetState(SlimeState.DIE);        
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        gameObject.tag = "Dead";
        audioSource.PlayOneShot(GameManager.Instance.soundManager.slimeDie);
        DropItem();
        StartCoroutine("FadeOutAndDestroy");
    }

    public void DropItem()
    {
        GameObject dropMoney = Instantiate(moneyItems[0], transform.position, Quaternion.identity); 
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
        gameObject.SetActive(false);
    }

    IEnumerator FadeInAndRespawn()
    {
        float startTime = Time.time;

        while (Time.time < startTime + lifeTime)
        {
            float normalizedTime = (Time.time - startTime) / lifeTime;
            Color currentColor = spriteRenderer.color;
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0f + normalizedTime);

            yield return null;
        }
    }

    IEnumerator KnockBackCoroutine()
    {
        GameObject target = GameObject.Find("Player");
        if (target != null)
        {
            Vector2 dir = ((Vector2)(target.transform.position - transform.position)).normalized * moveSpeed * 2;
            rb.velocity = -dir;
            yield return new WaitForSeconds(0.1f);
            rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(0.2f);
            SetState(SlimeState.MOVE);
            yield return null;
        }
    }

    IEnumerator ChooseAction(int time)
    {        
        if (isMoveable == true)
        {
            isMoveable = false;
            yield return new WaitForSeconds(time);
            SetState(SlimeState.MOVE);
            yield return null;
        }
        else
        {            
            yield return null;
        }
    }

    private void OnEnable()
    {
        currentHP = maxHP;
        state = SlimeState.IDLE;
        isMoveable = true;
        isHit = false;
        isDead = false;
        int move = UnityEngine.Random.Range(0, 2);
        if (move == 0)
        {
            moveRight = false;
        }
        else
        {
            moveRight = true;
        }
        StartCoroutine(FadeInAndRespawn());
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        gameObject.tag = "Enemy";
    }
}