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

    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        healthBar.fillAmount = currentHealth/maxHealth;

        if (currentHealth <= 0)
        {
            isDead = true;
            AudioManager.instance.Play("Death");
            GameManager.instance.RegisterKill();
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        enemy.Die();

        yield return new WaitForSeconds(2.5f);

        gameObject.SetActive(false);

    }
}
