using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum State
    {
        IDLE = 0,
        MOVE = 1,
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

    public bool isClimbAble = false;
    bool isJumping = false;
    bool isFacingRight = false;
    
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer; 
    Animator animator;
    Skill skill;
    public Transform attactPointLeft;
    public Transform attactPointRight;
    Transform currentAttackPoint = null;

    State state;

    public void SetState(State newState)
    {
        state = newState;
    }   
    
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
        rb.velocity = Vector3.zero;
        SetState(State.IDLE);
    }   
    private void Update()
    {
        switch(state)
        {
            case State.IDLE:
                Idle();
                break;
            case State.MOVE:
                Move();
                break;
            case State.ATTACK:
                break;
            case State.HIT:
                break;
            case State.CROUCH:
                break;
            case State.FALL:                
                break;
            case State.JUMP:
                Jump();
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
        animator.SetInteger("State", (int)state);
    }

    // TODO : 이동, 점프, 사다리타기, 아래점프, 위로 올라가기, 눕기, 스프라이트 뒤집기
    void Idle()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            SetState(State.MOVE);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            skill.Use("MeteorShower");
            SetState(State.ATTACK);
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
        
        if(x > 0)        { isFacingRight = true; }
        else if(x < 0)   { isFacingRight = false;}

        if (isFacingRight)
        {
            currentAttackPoint = attactPointRight;
            spriteRenderer.flipX = true;
        }
        else if(!isFacingRight)
        {
            currentAttackPoint = attactPointLeft;
            spriteRenderer.flipX = false;
        } 
        
        // TO IDLE
        if(Input.GetAxisRaw("Horizontal") == 0)
        {
            SetState(State.IDLE);
        }

        // TO JUMP
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            isJumping = true;
            SetState(State.JUMP);
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        SetState(State.FALL);
    }

    void Fall()
    {
        if(isJumping==false)
        {
            SetState(State.IDLE);
        }
    }

    public void Climb()
    {        
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            float y = Input.GetAxisRaw("Vertical");       
            rb.velocity = new Vector2(0, y * moveSpeed);
            print($"{rb.velocity}");
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            state = State.JUMP;
        }if(isClimbAble==false)
        {
            rb.gravityScale = 1;
            rb.velocity = Vector2.zero;
            SetState(State.FALL);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Platform")|| collision.collider.CompareTag("Ground"))
        {
            isJumping = false;
        }     
    }   
}
