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

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCol;
    private Vector2 movement;
    private float deltaX;
    private bool isJump;
    private bool grounded;
    private bool isPushing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        isJump |= Input.GetButtonDown("Jump");
        deltaX = horizontal * speed;

        //rotate sprite
        if (!Mathf.Approximately(deltaX, 0))
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);

        //check for ground
        Vector3 max = boxCol.bounds.max;
        Vector3 min = boxCol.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .05f);
        Vector2 corner2 = new Vector2(min.x, min.y - .1f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
        grounded = false;
        if (hit != null)
            grounded = true;

        //check for something to push
        Vector2 corner3 = new Vector2(max.x + (0.01f * Mathf.Sign(horizontal)), min.y + 0.01f);
        Vector2 corner4 = new Vector2(min.x + (0.01f * Mathf.Sign(horizontal)), min.y + 0.01f);
        Collider2D hitPush = Physics2D.OverlapArea(corner3, corner4, solidLayer);
        isPushing = false;
        if (hitPush != null)
            isPushing = true;

        #region Animation
        //running
        if (Mathf.Abs(deltaX) > minSpeedToRun)
        {
            anim.SetBool("isRunning", true);
        }
        else
            anim.SetBool("isRunning", false);
        //jumping
        if (Mathf.Abs(rb.velocity.y) > 0)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isAttack", false);
        }
        else
            anim.SetBool("isJumping", false);
        //Attack
        if (Input.GetKeyDown(KeyCode.F) && Mathf.Abs(rb.velocity.y) == 0)
        {
            anim.SetBool("isAttack", true);
        }
        //pushing
        if (horizontal != 0 && Mathf.Abs(rb.velocity.y) == 0 && isPushing)
        {
            anim.SetBool("isPushing", true);
        }
        else
            anim.SetBool("isPushing", false);
        #endregion
    }
    private void FixedUpdate()
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
        anim.SetBool("isAttack", false);
    }
}
