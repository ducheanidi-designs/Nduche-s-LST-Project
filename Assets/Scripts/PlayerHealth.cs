
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
   
   

    [SerializeField] private PlayerMov player;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       currentHealth = maxHealth; 

         
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;



       
    }

}
