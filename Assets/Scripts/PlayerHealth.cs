using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
    [SerializeField] private Image healthSlider;

    [SerializeField] private PlayerMov player;

       


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       currentHealth = maxHealth; 
         
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthSlider.fillAmount = currentHealth/maxHealth;

        if (currentHealth <= 0f)
        {
            AudioManager.instance.Play("Death");

            GameManager.instance.EndGame();
            
            gameObject.SetActive (false);
            
            Debug.Log("Player dead");
        }

           if (currentHealth <= 0f)
        {
            player.isAlive = false;
            player.Die();
        }

       
    }

}
