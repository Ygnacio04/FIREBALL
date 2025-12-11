using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 5;
    [SerializeField] public int currentHealth;

    void Start() {
        currentHealth = maxHealth;
        Time.timeScale = 1f; 
    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        Debug.Log($"current health: {currentHealth}");
        if (currentHealth <= 0) Die();
    }

    private void Die() {
        Debug.Log("GAME OVER");
        Time.timeScale = 0f; 
        Debug.Break(); 
    }
}