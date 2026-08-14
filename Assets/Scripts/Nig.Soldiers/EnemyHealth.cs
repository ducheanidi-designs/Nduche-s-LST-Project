using System.Collections;
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
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        Instantiate(explode, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(.5f);

        gameObject.SetActive(false);

    }
}
