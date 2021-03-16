using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private HitFlash hitFlash;
    [SerializeField] private Animator anim;
    [SerializeField] private float flashTime;

    public void Damage(int d)
    {
        if(health != null)
            health.TakeDamage(d);
        if(hitFlash != null && health.isAlive)
            hitFlash.MakeItFlash(flashTime);
        if (!health.isAlive)
            Dead();
    }

    private void Dead()
    {

    }
}
