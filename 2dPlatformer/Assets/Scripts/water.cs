using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    public GameObject player;
    
    void Start()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("player"))
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            playerRb.gravityScale = 0.3f;
        }
    }
}
