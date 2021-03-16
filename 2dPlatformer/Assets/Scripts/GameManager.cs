using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] HealthBar health;
    private PlayerStats playerStats;

    private void OnEnable()
    {
        // PlayerStats.OnUpdateScore += UpdateScore;
        PlayerStats.OnHealthChange += SetHealth;
    }

    private void OnDisable()
    {
        //  PlayerStats.OnUpdateScore -= UpdateScore;
        PlayerStats.OnHealthChange -= SetHealth;
    }

    void Start()
    {
        playerStats = PlayerStats.Singleton;
        health.SetMaxHealth(playerStats.maxHealth);
        SetHealth();
    }


    void Update()
    {
        
    }

    private void SetHealth()
    {
        health.SetHealth(playerStats.currentHealth);
        if (playerStats.currentHealth <= 0)
        {
            // death function
            //playerStats.isAlive = false;
        }
    }

}
