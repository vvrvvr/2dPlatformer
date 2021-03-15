using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [HideInInspector] public bool isInvulnerable;
    //private bool isAlive;

    private void Awake()
    {
        currentHealth = maxHealth;
        //isAlive = true;
        isInvulnerable = false;
    }

    public void TakeDamage(float incomingDamage)
    {
        if (isInvulnerable)
            return;
        currentHealth -= incomingDamage;
        CheckIsAlive();
    }

    private void CheckIsAlive()
    {
       // if (currentHealth < 0)
           // isAlive = false;
    }

    public void MakeInvulnerable(float time)
    {
        StartCoroutine(TimeToInvulnerable(time));
    }

    private IEnumerator TimeToInvulnerable(float time)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(time);
        isInvulnerable = false;
    }
}
