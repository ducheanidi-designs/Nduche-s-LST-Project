using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private GameObject bots;
    [SerializeField] private int botCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < botCount; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-range/2f, range/2f), 0, Random.Range(-range/2f, range/2f));
            GameObject bot = Instantiate (bots, transform.position + randomPos, Quaternion.identity);
        }

        if (GameManager.instance != null)
        {
            GameManager.instance.totalEnemiesToKill = (int)botCount;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(range, 1f, range));
    }
}
