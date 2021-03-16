using System;
using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public int totalScore; //hide in indspector all public vars
    [SerializeField] public int currentScore;
    [SerializeField] public int maxHealth;
    [SerializeField] public int currentHealth;
    [HideInInspector] public bool isInvulnerable;
    [HideInInspector] public bool isAlive;
    public static PlayerStats Singleton;
    public static Action OnDeath;
    public static Action OnHealthChange;
    public static Action OnUpdateScore;

    private void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;
        currentHealth = maxHealth;
        isInvulnerable = false;
       // DontDestroyOnLoad(gameObject);
    }

    public void UpdateTotalScore()
    {
        totalScore += currentScore;
        currentScore = 0;
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;
    }

    public void Score()
    {
        currentScore += 1;
        OnUpdateScore?.Invoke();
    }

    public void TakeDamage(int incomingDamage)
    {
        if (isInvulnerable)
            return;
        currentHealth -= incomingDamage;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        OnHealthChange?.Invoke();
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

    public void PlayerDeath()
    {
        isAlive = false;
        OnDeath?.Invoke();
    }
}
