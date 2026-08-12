using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private GameObject explode;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Instantiate(explode, transform.position, Quaternion.identity);
        }
    }
}
