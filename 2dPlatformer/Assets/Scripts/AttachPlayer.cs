using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPlayer : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "character")
        {
            player = collision.GetComponent<Player>();
            player.platformVelocity = rb.velocity;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "character")
        {
            player.platformVelocity = rb.velocity;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "character")
        {
            player.platformVelocity = Vector2.zero;
            player = null;
        }
    }

}
