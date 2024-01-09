using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum State
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

    public bool isClimbAble = false;
    
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer; 
    Animator animator;
    Skill skill;

    State state;

    public void SetState(State newState)
    {
        state = newState;
    }

    // 발판제어용 변수
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
        SetState(State.IDLE);
    }   
    private void Update()
    {
        switch(state)
        {
            case State.IDLE:
                if(Input.GetAxisRaw("Horizontal")!=0)
                {
                    SetState(State.MOVE);
                }
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    skill.Use("MeteorShower");
                    SetState(State.ATTACK);
                }
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    skill.Use("FireArrow");
                    state = State.ATTACK;
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    skill.Use("Teleport");
                }

                if (Input.GetKeyDown(KeyCode.LeftAlt))
                {
                    rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
                    state = State.JUMP;
                }

                break;
            case State.MOVE:
                Move();
                break;
            case State.ATTACK:
                SetState(State.IDLE);
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
                Climb();
                break;
            case State.LADDER: 
                break;
            case State.DEAD:
                break;
            case State.SIT:
                break;             
        }
    }

    // TODO : 이동, 점프, 사다리타기, 아래점프, 위로 올라가기, 눕기, 스프라이트 뒤집기
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);       
        
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
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            state = State.IDLE;
        }
        if (isClimbAble == true)
        {
            if(Input.GetAxisRaw("Vertical") != 0)
            {
                SetState(State.CLIMB);
                rb.gravityScale = 0;
            }
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

        if (collision.collider.CompareTag("Platform") == true)
        {
            currentPlatform = collision.collider.GetComponent<PlatformControl>();
        }
    }   
}
