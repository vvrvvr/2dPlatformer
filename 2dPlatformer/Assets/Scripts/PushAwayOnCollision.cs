using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAwayOnCollision : MonoBehaviour
{
    [SerializeField] private float impulseForce;
    [SerializeField] private float x;
    [SerializeField] private float y;
    public bool check;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "character" || collision.gameObject.name == "crate (2)")
        {
            Debug.Log("trigger");
            collision.gameObject.GetComponent<Player>().HasControl = false;
            var playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if(check)
                playerRb.velocity = Vector2.zero;
            playerRb.AddForce(new Vector2(x, y) * impulseForce, ForceMode2D.Impulse);
        }
    }
}
