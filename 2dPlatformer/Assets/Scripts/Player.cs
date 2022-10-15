using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float throwForce;
    [SerializeField] private float minSpeedToRun;
    [SerializeField] private LayerMask solidLayer;
    [SerializeField] private float groundOffset;
    [SerializeField] private float pushOffset;
    [SerializeField] private GameObject bombPrefab;
    [HideInInspector] public bool HasControl;
    [HideInInspector] public float platformVelocity;
    [HideInInspector] public bool grounded;

    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D boxCol;
    private PlayerAttack playerAttack;
    private Vector2 movement;
    private Vector2 boundsMax;
    private Vector2 boundsMin;
    private float deltaX;
    private float horizontal;
    private float currentDir = 1;
    private float pushOffsetY = 0.4f;
    private bool isJump;
    private bool isPushing;

    // private bool isAttack;
    // private bool isBomb;
    // private float attackRate = 4f;
    // private float nextAttackTime = 0;
    private void OnEnable()
    {
        PlayerStats.OnDeath += Death;
    }

    private void OnDisable()
    {
        PlayerStats.OnDeath -= Death;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCol = GetComponent<CapsuleCollider2D>();
        //playerAttack = GetComponent<PlayerAttack>();
        HasControl = true;
        platformVelocity = 0;
    }

    void Update()
    {
        if (HasControl)
        {
            horizontal = currentDir;
            isJump |= Input.GetButtonDown("Jump");
            // if (Time.time >= nextAttackTime)
            // {
            //     if (Input.GetKeyDown(KeyCode.F))
            //     {
            //         isAttack = true;
            //         nextAttackTime = Time.time + 1f / attackRate;
            //     }
            // }
            //isBomb |= Input.GetKeyDown(KeyCode.B);
        }
        else
        {
            horizontal = 0;
            isJump = false;
            //isAttack = false;
        }

        deltaX = horizontal * speed;
        boundsMax = boxCol.bounds.max;
        boundsMin = boxCol.bounds.min;

        CheckGround();
        CheckToPush(horizontal);
        PlayerAnimation(horizontal);
    }

    private void ThrowBomb(bool check)
    {
        if (!check)
            return;

        var dir = transform.localScale.x;
        Vector2 throwDirection = new Vector2(dir, 1f);
        var bomb = Instantiate(bombPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
        Rigidbody2D bombRb = bomb.GetComponent<Rigidbody2D>();
        bombRb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
        // isBomb = false;
    }

    private void FixedUpdate()
    {
        if (HasControl)
            MoveCharacter();
        // ThrowBomb(isBomb);
    }

    private void CheckGround()
    {
        Vector2 corner1 = new Vector2(boundsMax.x - 0.06f, boundsMin.y - groundOffset); //magic numbers
        Vector2 corner2 = new Vector2(boundsMin.x + 0.06f, boundsMin.y - groundOffset);
        Debug.DrawLine(corner1, corner2, Color.red, 0.001f, false);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2, solidLayer);
        grounded = false;
        if (hit != null)
        {
            grounded = true;
        }
        // rb.gravityScale = grounded && deltaX == 0 ? 0 : 1;
    }

    private void CheckToPush(float horizontalAxisInput)
    {
        float dir = 0;
        if (horizontalAxisInput != 0)
            dir = Mathf.Sign(horizontalAxisInput);
        Vector2 corner3 = new Vector2(boundsMax.x + dir * pushOffset, boundsMin.y + pushOffsetY);
        Vector2 corner4 = new Vector2(boundsMin.x + dir * pushOffset, boundsMin.y + pushOffsetY);
        Debug.DrawLine(corner3, corner4, Color.green, 0.001f, false);
        Collider2D hitPush = Physics2D.OverlapArea(corner3, corner4, solidLayer);
        isPushing = false;
        if (hitPush != null)
        {
            isPushing = true;
            currentDir *= -1;
        }
            
    }

    private void PlayerAnimation(float horizontalAxisInput)
    {
        //rotate sprite
        if (!Mathf.Approximately(deltaX, 0))
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        //running
        if (Mathf.Abs(deltaX) > minSpeedToRun)
        {
            anim.SetBool("isRunning", true);
        }
        else
            anim.SetBool("isRunning", false);
        //jumping
        if (!grounded)
        {
            if (rb.velocity == Vector2.zero) //предотвратить залипания на углах платформ
                deltaX *= -0.5f;
            anim.SetBool("isJumping", true);
            anim.ResetTrigger("Attack");
        }
        else
            anim.SetBool("isJumping", false);
        
        
        
        //pushing
        if (horizontalAxisInput != 0 && grounded && isPushing)
        {
            anim.SetBool("isPushing", true);
        }
        else
            anim.SetBool("isPushing", false);
    }

    private void MoveCharacter()
    {
        movement = new Vector2(deltaX + platformVelocity, rb.velocity.y);

        if (isJump && grounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
        rb.velocity = movement;
        isJump = false;
    }

    // public void AttackEnd()
    // {
    //     anim.SetTrigger("Attack");
    // }

    public void LoseControl(float time)
    {
        StartCoroutine(WaitToChangeHasControl(time));
    }
    private IEnumerator WaitToChangeHasControl(float time)
    {
        HasControl = false;
        yield return new WaitForSeconds(time);
        HasControl = true;
    }

    private void Death()
    {
        anim.SetTrigger("dead");
        this.enabled = false;
    }

}
