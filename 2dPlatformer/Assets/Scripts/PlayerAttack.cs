using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private int playerDamage;
    [SerializeField] private LayerMask EnemyLayers;

    public void Attack()
    {
        Collider2D[] EnemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, EnemyLayers);
        foreach (var element in EnemiesHit)
        {
            var enemy = element.GetComponent<Enemy>();
            if(enemy != null)
                enemy.Damage(playerDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
