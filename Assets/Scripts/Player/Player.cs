using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int currentHP;
    public int currentMP;
    public int currentEXP;
    public int maxHP;
    public int maxMP;
    public int maxEXP;

    public float moveSpeed;
    public float jumpPower;
       
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer; 
    Animator animator;
    Skill skill;

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
    }   
    private void Update()
    {
        Move();
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
        }

        if(Input.GetKey(KeyCode.Space))
        {
            skill.Use("Teleport");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform") == true)
        {
            currentPlatform = collision.collider.GetComponent<PlatformControl>();
        }
    }
}
