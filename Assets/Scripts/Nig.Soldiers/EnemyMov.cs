using UnityEngine;
using UnityEngine.AI;

public class EnemyMov : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Transform[] wayPoints;

    [SerializeField] private bool inRange;

    [SerializeField] private float dis;

    [SerializeField] private float disToPlayer;

    [SerializeField] private GameObject impact;

    [SerializeField] private Animator anim;

    [SerializeField] private float fireRate;

    private float nextTimeToFire;

    public int wayP = 0;  

    public int randNum;

    public int botIndex = 0;

    private Ray ray;

    int die;

    public bool isAlive;

    private PlayerMov player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        randNum = Random.Range(0, wayPoints.Length);
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.forward); 

        
        CheckPlayerDis();
        wayP = randNum;

        dis = Vector3.Distance (transform.position, wayPoints[wayP].transform.position);

        if (!inRange)
        {
            anim.SetFloat("InputY", 1f);
            agent.SetDestination(wayPoints[wayP].position);
            

        if (dis < 5)
        {
        Debug.Log("Turn");
        randNum = Random.Range(0, wayPoints.Length);

                // if(wayP == wayPoints.Length)
                // {
                //     wayP = 0;
                // }
        }
        }
        else
        {
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire =Time.time + 1 / fireRate;
                Shoot();
            }
            
            agent.SetDestination(target.position);
            anim.SetFloat("InputY", 1f);

            if (disToPlayer <= agent.stoppingDistance)
            {
                anim.SetFloat("InputY", 0f);

                Vector3 tarDir = target.position - transform.position;
                tarDir.y = 0f;
                if (tarDir != Vector3.zero)
                {
                    Quaternion tarRot = Quaternion.LookRotation(tarDir.normalized);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, tarRot, 200f * Time.deltaTime);
                }
            }
        }

       
    }

    void CheckPlayerDis()
    {
        disToPlayer = Vector3.Distance(transform.position, target.position);

        if (disToPlayer < 10)
        {
            inRange = true;
        } else if (disToPlayer >= 25)
        {
            inRange = false;
        }
    }
    
    void Shoot()
    {
        if (Physics.Raycast(ray, out RaycastHit hit, 999f))
        {
            // Debug.Log(hit.collider.name);

            Debug.DrawLine(transform.position, hit.point, Color.blue);

            PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();

            if(playerHealth != null)
            {
                Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
                playerHealth.TakeDamage(20);
            }
        }
    }
}
