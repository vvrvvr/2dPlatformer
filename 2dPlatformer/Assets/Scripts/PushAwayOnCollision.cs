using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAwayOnCollision : MonoBehaviour
{
    [SerializeField] private int damage;
    private float impulseForce = 10f;
    private float x = 0.5f;
    private float y = 1f;
    private float xCenter;

    private void Awake()
    {
        xCenter = GetComponent<Collider2D>().bounds.center.x;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "character")
        {
            var player = collision.gameObject.GetComponent<Player>();
            var playerTransform = collision.gameObject.GetComponent<Transform>();
            var playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            var dir = Mathf.Sign(playerTransform.position.x - xCenter);

            player.LoseControl(0.4f);
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(new Vector2(x * dir, y) * impulseForce, ForceMode2D.Impulse);
        }
    }
}
