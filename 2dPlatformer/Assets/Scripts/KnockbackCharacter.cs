using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(Health), typeof(HitFlash))]
public class KnockbackCharacter : MonoBehaviour
{
    [SerializeField] float knockbackTime;
    private Player player;
    private Transform playerTransform;
    private Rigidbody2D playerRb;
    private HitFlash flash;
    private Health health;

    private float x = 0.5f;
    private float y = 1f;
    private float impulseForce = 10f;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerTransform = GetComponent<Transform>();
        playerRb = GetComponent<Rigidbody2D>();
        flash = GetComponent<HitFlash>();
        health = GetComponent<Health>();
    }

    public void KnockbackPlayer(float damage, float centerOfAnotherObject)
    {
        if (health.isInvulnerable)
            return;

        flash.MakeItFlash(knockbackTime);
        health.TakeDamage(damage);
        health.MakeInvulnerable(knockbackTime);
        player.LoseControl(0.4f); // magic number for knockback
        playerRb.velocity = Vector2.zero;
        var dir = Mathf.Sign(playerTransform.position.x - centerOfAnotherObject);
        playerRb.AddForce(new Vector2(x * dir, y) * impulseForce, ForceMode2D.Impulse);
    }
}
