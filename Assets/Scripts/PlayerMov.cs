using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMov : MonoBehaviour
{
  
  [SerializeField] private float moveSpeed;
  [SerializeField] private float rotateSpeed;

  [SerializeField] private float weaponDamage;
  [SerializeField] private ParticleSystem muzzleFlash;
  [SerializeField] private GameObject impact;

  [SerializeField] private PlayerInput playerInput;
  [SerializeField] private InputAction moveAction;
  [SerializeField] private InputAction lookAction;
  [SerializeField] private InputAction attackAction;

  [SerializeField] private Transform camTarget;
  [SerializeField] private Vector2 pitchClampValue;

  [SerializeField] private Animator anim;

  [SerializeField] private Transform debugSphere;

  [SerializeField] private float fireRate;
  [SerializeField] private int maxAmmo;
  [SerializeField] private int ammo;
  [SerializeField] private TextMeshProUGUI ammoText;
  [SerializeField] private float coolDownTime;


  private float nextTimeToFire;

  private float xRotation = 0f;

  int die;

  public bool isAlive;

  void Start()
  {
    Cursor.lockState = CursorLockMode.Locked;

    ammo = maxAmmo;

    moveAction = playerInput.actions["Move"];
    lookAction = playerInput.actions["Look"];
    attackAction = playerInput.actions["Attack"];

    die = Animator.StringToHash("Die");

    isAlive = true;

  }

  void Update()
  {
    ammoText.text = ammo.ToString();

    Vector2 inputVector = moveAction.ReadValue<Vector2>();

  

    anim.SetFloat("InputX", inputVector.x);
    anim.SetFloat("InputY", inputVector.y);

    if(isAlive)
    {
      if (inputVector.magnitude > 0)
      {
        AudioManager.instance.Play("Running");
      }
      else
      {
        AudioManager.instance.Stop("Running");
      }
    }

    inputVector = inputVector.normalized;

    Vector3 movDir = transform.right * inputVector.x + transform.forward * inputVector.y;
    transform.position += movDir * moveSpeed * Time.deltaTime;

    Vector2 lookInput = lookAction.ReadValue<Vector2>();

    //Rotate the player
    HandleRotation();
    //transform.forward = Vector3.Slerp(transform.forward, movDir, rotateSpeed * Time.deltaTime);

    Vector2 screenCenter = new Vector2(Screen.width/2f, Screen.height/2f);

    Ray ray = Camera.main.ScreenPointToRay(screenCenter);

    if (Physics.Raycast(ray, out RaycastHit hitInfo, 999f))
      {
        debugSphere.position = hitInfo.point;
        Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.red);
      }
    
      if (attackAction.IsPressed() && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1/fireRate;
                Shoot(ray);
          }
   }

    void Shoot (Ray ray)
    {
      if (ammo > 0)
      {
         ammo -= 1;
         muzzleFlash.Play();
         AudioManager.instance.PlayOneShot("Shoot");
        if (Physics. Raycast(ray, out RaycastHit hitInfo, 999f))
        {
          Instantiate(impact, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
          EnemyHealth enemyHealth = hitInfo.collider.GetComponentInParent<EnemyHealth>();


          if (enemyHealth != null)
          {
            enemyHealth.TakeDamage(weaponDamage); 
          }
        }
      } else
      {
        StartCoroutine(CoolDown(coolDownTime));
      }
    }

  public void Die()
  {
    anim.CrossFade(die, .025f);
  }

  void HandleRotation()
  {

    Vector2 lookInput = lookAction.ReadValue<Vector2>();

    // LEFT/RIGHT - Rotate camera target (and player follows)
    transform.Rotate(Vector3.up * lookInput.x);

    // player faces camera direction
    float camYaw = transform.eulerAngles.y;
    transform.rotation = Quaternion.Euler(0, camYaw, 0);

    //UP/DOWN
    xRotation -= lookInput.y;
    xRotation = Mathf. Clamp(xRotation, -pitchClampValue.x, pitchClampValue.y);
    camTarget.rotation = Quaternion.Euler(xRotation, camYaw, 0f);
  }

  IEnumerator CoolDown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ammo = maxAmmo;
    }

}

