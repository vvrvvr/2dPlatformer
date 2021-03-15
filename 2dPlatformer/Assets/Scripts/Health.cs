using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    private bool isAlive;

    private void Awake()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }

    public void TakeDamage(float incomingDamage)
    {
        currentHealth -= incomingDamage;
        CheckIsAlive();
    }

    private void CheckIsAlive()
    {
        if (currentHealth < 0)
            isAlive = false;
    }
}
