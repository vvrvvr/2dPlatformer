using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] HealthBar health;
    [SerializeField] GameObject deathMenu;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Player player;
    private PlayerStats playerStats;

    private void OnEnable()
    {
        // PlayerStats.OnUpdateScore += UpdateScore;
        PlayerStats.OnHealthChange += SetHealth;
        PlayerStats.OnUpdateScore += UpdateScore;
    }

    private void OnDisable()
    {
        //  PlayerStats.OnUpdateScore -= UpdateScore;
        PlayerStats.OnHealthChange -= SetHealth;
        PlayerStats.OnUpdateScore -= UpdateScore;
    }

    private void UpdateScore()
    {
        text.text = ""+ playerStats.totalScore;
    }

    void Start()
    {
        playerStats = PlayerStats.Singleton;
        health.SetMaxHealth(playerStats.maxHealth);
        SetHealth();
        player.HasControl = false;
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

    public void SetPlayerControls(bool controlsState)
    {
        player.HasControl = controlsState;
    }
    

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
