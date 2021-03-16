using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(HitFlash))]
public class KnockbackCharacter : MonoBehaviour
{
    [SerializeField] float knockbackTime;
    private Player player;
    private Transform playerTransform;
    private Rigidbody2D playerRb;
    private HitFlash flash;
    private PlayerStats playerstats;

    private float x = 0.5f;
    private float y = 1f;
    private float impulseForce = 10f;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerstats = PlayerStats.Singleton;
        playerTransform = GetComponent<Transform>();
        playerRb = GetComponent<Rigidbody2D>();
        flash = GetComponent<HitFlash>();
    }

    public void KnockbackPlayer(int damage, float centerOfAnotherObject)
    {
        flash.MakeItFlash(knockbackTime);
        playerstats.TakeDamage(damage);
        playerstats.MakeInvulnerable(knockbackTime);
        player.LoseControl(0.4f); // magic number for knockback
        playerRb.velocity = Vector2.zero;
        var dir = Mathf.Sign(playerTransform.position.x - centerOfAnotherObject);
        playerRb.AddForce(new Vector2(x * dir, y) * impulseForce, ForceMode2D.Impulse);
    }
}
