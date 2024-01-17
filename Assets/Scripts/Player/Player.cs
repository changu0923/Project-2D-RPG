using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public enum State
    {
        IDLE    = 0,
        MOVE    = 1,
        ATTACK  = 2,
        HIT     = 3,
        CROUCH  = 4,
        FALL    = 5,
        JUMP    = 6,
        CLIMB   = 7,
        LADDER  = 8,
        DEAD    = 9,
        SIT     = 10,
    }

    public enum AttackMotion
    {
        NORMAL  = 0,
        BOW     = 1,
        SWING   = 2,
    }

    public string playerName = "zl존법사";

    public int currentHP;
    public int currentMP;
    public int currentEXP;
    public int maxHP;
    public int maxMP;
    public int maxEXP;
    public int money;
    public int level = 1;

    public float moveSpeed;
    public float jumpPower;

    public bool isClimbAble = false;
    public bool isJumping = false;
    public bool isFacingRight = false;
    public bool isMoveAble;
    public bool isHit;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Skill skill;
    TextMeshProUGUI nameText;

    public DropInfoText dropInfoText;
    public GameObject levelUpEffectPrefab;
    public Transform attactPointLeft;
    public Transform attactPointRight;
    public Transform currentAttackPoint;
    public GameObject tombPrefab;

    public State state;
    public AttackMotion attackMotion;

    public void SetState(State newState)
    {
        state = newState;
    }   

    public void SetAttackMotion(AttackMotion newMotion)
    {
        attackMotion = newMotion;
    }    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        skill = GetComponent<Skill>();   
        nameText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        // nameText.text = playerName;
        currentAttackPoint = attactPointLeft; ;
        currentHP = maxHP;
        currentMP = maxMP;
        moveSpeed = 1.5f;
        jumpPower = 5f; // 4.5f;
        rb.velocity = Vector3.zero;
        isMoveAble = true;        
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
                Attack();
                break;
            case State.HIT:
                break;
            case State.CROUCH:
                break;
            case State.FALL:
                Fall();
                break;
            case State.JUMP:
                Jump();
                break;
            case State.CLIMB:
                break;
            case State.LADDER: 
                break;
            case State.DEAD:
                Die();
                break;
            case State.SIT:
                break;             
        }   animator.SetInteger("State", (int)state);
        CheckOnGround();
    }

    // TODO : 이동, 점프, 사다리타기, 아래점프, 위로 올라가기, 눕기, 스프라이트 뒤집기
    void Idle()
    {
        if (isMoveAble == true) 
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                SetState(State.MOVE);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                skill.Use("MeteorShower");
                SetAttackMotion(AttackMotion.NORMAL);
                SetState(State.ATTACK);
            }

            if(Input.GetKeyDown(KeyCode.Z)) 
            {
                attackMotion = AttackMotion.SWING;
                SetState(State.ATTACK);
                skill.Use("MagicClaw");                
            }


            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                skill.Use("FireArrow");
                SetAttackMotion(AttackMotion.BOW);
                SetState(State.ATTACK);
            }

            // TO JUMP
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                SetState(State.JUMP);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
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
            SetState(State.JUMP);
        }
    }

    void Attack()
    {
        // TODO : Attack -> 각 모션 설정 필요
        switch(attackMotion)
        {
            case AttackMotion.NORMAL:
                animator.SetInteger("AttackMotion", (int)attackMotion);
                StandAttack();                
                break;
            case AttackMotion.BOW:
                animator.SetInteger("AttackMotion", (int)attackMotion);
                BowAttack();
                break;
            case AttackMotion.SWING:
                int motionNumber = Random.Range(0, 4);
                animator.SetInteger("AttackMotion", (int)attackMotion);
                animator.SetInteger("Motion", motionNumber); 
                break;
        }
    }

    void BowAttack()
    {
        SetState(State.IDLE);
    }

    void StandAttack()
    {
        SetState(State.IDLE);
    } 

    void Jump()
    {
        isJumping = true; 
        animator.SetBool("isJumping", true);
        rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        SetState(State.FALL);
    }

    void Fall()
    {
        // 땅에 닿았을 때
        if(isJumping==false)
        {            
            animator.SetBool("isJumping", false);
            SetState(State.IDLE);
        }        
    }
    
    void Climb()
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

    void Die()
    {
        rb.velocity = Vector2.zero;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        spriteRenderer.color = new Color(0f, 0f, 0f, 0f);
        SetState(State.DEAD);
        isMoveAble = false;
    }

    public void TakeDamage(float damage)
    {
        if (isHit == false)
        {
            if (state != State.DEAD)
            {
                currentHP -= (int)damage;
                int knockbackDir = isFacingRight ? -1 : 1;
                rb.AddForce(new Vector2((float)(knockbackDir * 1.5), 1.5f) * 2.5f, ForceMode2D.Impulse);
                isHit = true;
                isJumping = true;
                state = State.FALL;
                StartCoroutine("OnHitLayerChangeCoroutine");
                StartCoroutine("OnHitColorChangeCoroutine");

                if(currentHP <=0)
                {
                    currentHP = 0;
                    Vector3 up = new Vector3(0f, 1f, 0f);
                    Vector3 spawnLocation = gameObject.transform.position + up;
                    GameObject tomb = Instantiate(tombPrefab, spawnLocation, Quaternion.identity);
                    tomb.transform.parent = gameObject.transform;
                    Die();
                }
            }
        }
        else
        {
            CheckOnGround();
        }
    }

    public void GainExp(int exp)
    {
        currentEXP += exp;
        dropInfoText.UpdateExpText(exp);
        if (currentEXP>=maxEXP) 
        {
            currentEXP -= maxEXP;
            LevelUP();            
        }
    }

    public void EarnMoney(int money)
    {
        this.money += money;
        dropInfoText.UpdateMoneyText(money);
    }

    public void LevelUP()
    {
        GameObject levelupEffect = Instantiate(levelUpEffectPrefab, transform.position, Quaternion.identity);                
        levelupEffect.transform.parent = gameObject.transform;
        level += 1;
        GameObject ui = GameObject.Find("LevelNumber");
        ui.GetComponent<UILevelNumber>().UpdateLevelImage();
        maxEXP = maxEXP + (int)(maxEXP * 0.1);
        currentHP = maxHP;
        currentMP = maxMP;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Platform")|| collision.collider.CompareTag("Ground"))
        {
            isJumping = false;
        }
        if(collision.collider.CompareTag("Enemy")==true)
        {
            if (collision.collider.TryGetComponent(out Enemy monster))
            {
                TakeDamage(monster.damage);                
            }
        }
    }

    private void CheckOnGround()
    {        
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        Vector2 center = new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.min.y);
        RaycastHit2D hit = Physics2D.Raycast(center, Vector2.down);
        float distance = Vector2.Distance(center, hit.point);
        if(distance == 0.1)
        {
            isJumping = false;
        }        
    }

    IEnumerator OnHitLayerChangeCoroutine()
    {        
        gameObject.layer = LayerMask.NameToLayer("Hit");
        yield return new WaitForSeconds(3);
        gameObject.layer = LayerMask.NameToLayer("Player");
        isHit = false;        
        yield return null;
    }

    IEnumerator OnHitColorChangeCoroutine()
    {
        Color initColor = new Color(1f, 1f, 1f, 1f);  
        Color blinkColor = new Color(0.39f, 0.39f, 0.39f, 1f);
        for(int i = 0; i<5; i++)
        {
            spriteRenderer.color = blinkColor;
            yield return new WaitForSeconds(.15f);
            spriteRenderer.color = initColor;
            yield return new WaitForSeconds(.15f);
        }
        spriteRenderer.color = initColor;
    }

    IEnumerator OnDieCoroutine()
    {
        yield return new WaitForSeconds(3f);
        // TODO : 사망팝업호출
        yield return null;
    }
}
