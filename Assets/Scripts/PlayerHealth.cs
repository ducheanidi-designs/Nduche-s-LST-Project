using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
    [SerializeField] private Image healthSlider;
       


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
            Debug.Log("Player dead");
        }

       
    }

}
