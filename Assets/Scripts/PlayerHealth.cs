using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
    [SerializeField] private Image healthSlider;

    [SerializeField] private PlayerMov player;

    private bool isDead = false;   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       currentHealth = maxHealth; 
         
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        healthSlider.fillAmount = currentHealth/maxHealth;

        if (currentHealth <= 0f)
        {
            isDead = true;
            StartCoroutine(Die());
                        
        }

    }

    IEnumerator Die()
    {
        Debug.Log("Player death sequence started.");
        
        AudioManager.instance.PlayOneShot("Death");

        player.isAlive = false;
        player.Die();

        yield return new WaitForSecondsRealtime(2.5f);

        gameObject.SetActive(false);
        GameManager.instance.EndGame();


    }

}
