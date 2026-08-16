using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private GameObject explode;

    [SerializeField] private Image healthBar;

    [SerializeField] private EnemyMov enemy;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.fillAmount = currentHealth/maxHealth;

        if (currentHealth <= 0)
        {
            AudioManager.instance.Play("Death");
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        enemy.Die();

        yield return new WaitForSeconds(5f);

        gameObject.SetActive(false);

    }
}
