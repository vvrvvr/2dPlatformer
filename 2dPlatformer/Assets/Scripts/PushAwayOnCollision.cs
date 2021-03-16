using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAwayOnCollision : MonoBehaviour
{
    [SerializeField] private int damage;
    private float xCenter;

    private void Awake()
    {
        xCenter = GetComponent<Collider2D>().bounds.center.x;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "character")
        {
            if (PlayerStats.Singleton.isInvulnerable)
                return;
            collision.GetComponent<KnockbackCharacter>().KnockbackPlayer(damage, xCenter);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "character")
        {
            if (PlayerStats.Singleton.isInvulnerable)
                return;
            collision.GetComponent<KnockbackCharacter>().KnockbackPlayer(damage, xCenter);
        }
    }
}
