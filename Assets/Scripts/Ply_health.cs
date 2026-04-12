using UnityEngine;
using UnityEngine.UI;



public class PlayerHealth : MonoBehaviour
{   
    public int maxHealth = 100;
    public bool isInsideHouse = false;
    public int currentHealth;
   // private bool isHealing;
   public Slider healthBar;
   

    void Start()
    {
        currentHealth = maxHealth;

        //UI OF HEALTH BAR
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }

        //HEALTH BAR
        healthBar.value = currentHealth;
    }



    void Die()
{
    Debug.Log("Player Died");

    // Stop movement completely
    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    rb.linearVelocity = Vector2.zero;

    // Optional: stop physics interactions
    rb.simulated = false;

    // Disable scripts
    GetComponent<Playermovement>().enabled = false;

    // Optional: disable attack script too
    // GetComponent<PlayerAttack>().enabled = false;
    GameManager.instance.GameOver();
}




public void StartHealing()
{
    isHealing = true;
    //UI
    healthBar.value = currentHealth;
}

public void StopHealing()
{
    isHealing = false;
}




public bool isHealing = false;
public float healRate = 10f;

float healTimer = 0f;








void Update()
{
    if (isHealing && currentHealth < maxHealth)
    {
        healTimer += Time.deltaTime;

        if (healTimer >= 0.2f)
        {
            currentHealth += 2;
            healTimer = 0f;
        }
    }
    //UI
    healthBar.value = currentHealth;
}

}
