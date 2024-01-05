using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum State
    {
        IDLE,
        MOVE,
        ATTACK,
        HIT,
        CROUCH,
        FALL,
        JUMP,
        CLIMB,
        LADDER,
        DEAD,
        SIT,
    }

    public int currentHP;
    public int currentMP;
    public int currentEXP;
    public int maxHP;
    public int maxMP;
    public int maxEXP;

    public float moveSpeed;
    public float jumpPower;

    private float attackCoolTime;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer; 
    Animator animator;
    Skill skill;

    State state;

    // ��������� ����
    bool isJumping;
    PlatformControl currentPlatform;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        skill = GetComponent<Skill>();   
    }

    private void Start()
    {
        moveSpeed = 1.5f;
        jumpPower = 5f; // 4.5f;
        state = State.IDLE;
    }   
    private void Update()
    {
        switch(state)
        {
            case State.IDLE:
                if(Input.GetAxisRaw("Horizontal")!=0)
                {
                    state = State.MOVE;
                }

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    skill.Use("FireArrow");
                    attackCoolTime = 5f;
                    state = State.ATTACK;
                }
              
                break;
            case State.MOVE:
                Move();
                break;
            case State.ATTACK:
                StartCoroutine("WaitingForCoolTimeCoroutine");
                break;
            case State.HIT:
                break;
            case State.CROUCH:
                break;
            case State.FALL:
                if (isJumping == false)
                {
                    rb.velocity = Vector3.zero;
                    state = State.IDLE;
                }
                break;
            case State.JUMP:
                isJumping = true;
                state = State.FALL;
                break;
            case State.CLIMB: 
                break;
            case State.LADDER: 
                break;
            case State.DEAD:
                break;
            case State.SIT:
                break;             
        }
    }

    // TODO : �̵�, ����, ��ٸ�Ÿ��, �Ʒ�����, ���� �ö󰡱�, ����, ��������Ʈ ������
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);       
        if(rb.velocity.x == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
        spriteRenderer.flipX = x < 0;

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {            
            rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            state = State.JUMP;
        }

        if(Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.DownArrow))
        {
            if(currentPlatform!=null) 
            {
                currentPlatform.PlayerDown();
            }
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            skill.Use("FireArrow");
            attackCoolTime = 5f;
            state = State.ATTACK;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            skill.Use("Teleport");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Platform")|| collision.collider.CompareTag("Ground"))
        {
            isJumping = false;
        }

        if (collision.collider.CompareTag("Platform") == true)
        {
            currentPlatform = collision.collider.GetComponent<PlatformControl>();
        }
    }

    IEnumerator WaitingForCoolTimeCoroutine()
    {
        rb.velocity = Vector3.zero;
        while(attackCoolTime<=0)
        {
            attackCoolTime -= Time.deltaTime;
            rb.velocity = Vector3.zero;
        }
        attackCoolTime = 0f;
        state = State.IDLE;
        yield return null;
    }
}
