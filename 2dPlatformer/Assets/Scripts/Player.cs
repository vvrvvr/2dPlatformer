using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float minSpeedToRun;
    [SerializeField] private LayerMask solidLayer;
    [SerializeField] private float groundOffset;
    [SerializeField] private float pushOffset;

    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D boxCol;
    private Vector2 movement;
    private Vector2 boundsMax;
    private Vector2 boundsMin;
    private float deltaX;
    private bool isJump;
    private bool grounded;
    private bool isPushing;
    private bool isAttack;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCol = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        isJump |= Input.GetButtonDown("Jump");
        isAttack = Input.GetKeyDown(KeyCode.F);

        deltaX = horizontal * speed;
        boundsMax = boxCol.bounds.max;
        boundsMin = boxCol.bounds.min;

        CheckGround();
        CheckToPush(horizontal);
        PlayerAnimation(horizontal);
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void CheckGround()
    {
        Vector2 corner1 = new Vector2(boundsMax.x - 0.025f, boundsMin.y - groundOffset);
        Vector2 corner2 = new Vector2(boundsMin.x + 0.025f, boundsMin.y - groundOffset);
       // Debug.DrawLine(corner1, corner2, Color.red, 0.001f, false);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
        grounded = false;
        if (hit != null)
            grounded = true;
    }

    private void CheckToPush(float horizontalAxisInput)
    {
        float dir = 0;
        if (horizontalAxisInput != 0)
            dir = Mathf.Sign(horizontalAxisInput);
        Vector2 corner3 = new Vector2(boundsMax.x + dir * pushOffset, boundsMin.y + 0.1f);
        Vector2 corner4 = new Vector2(boundsMin.x + dir * pushOffset, boundsMin.y + 0.1f);
        Debug.DrawLine(corner3, corner4, Color.green, 0.001f, false);
        Debug.Log(dir);
        Collider2D hitPush = Physics2D.OverlapArea(corner3, corner4, solidLayer);
        isPushing = false;
        if (hitPush != null)
            isPushing = true;
        
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
            anim.SetBool("isJumping", true);
            anim.ResetTrigger("Attack");
        }
        else
            anim.SetBool("isJumping", false);
        //Attack
        if (isAttack && Mathf.Abs(rb.velocity.y) == 0)
        {
            anim.SetTrigger("Attack");
            isAttack = false;
        }
        //pushing
        if (horizontalAxisInput != 0 && Mathf.Abs(rb.velocity.y) == 0 && isPushing)
        {
            anim.SetBool("isPushing", true);
        }
        else
            anim.SetBool("isPushing", false);
    }

    private void MoveCharacter()
    {
        movement = new Vector2(deltaX, rb.velocity.y);
        if (isJump && grounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
        rb.velocity = movement;
        isJump = false;
    }

    public void AttackEnd()
    {
        anim.SetTrigger("Attack");
    }
}
