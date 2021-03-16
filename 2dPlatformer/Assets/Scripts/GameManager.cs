using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] HealthBar health;
    [SerializeField] GameObject deathMenu;
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

    private void SetHealth()
    {
        health.SetHealth(playerStats.currentHealth);
        if (playerStats.currentHealth <= 0)
        {
            playerStats.PlayerDeath();
            deathMenu.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
