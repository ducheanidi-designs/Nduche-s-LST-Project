using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerMov : MonoBehaviour
{
  
  [SerializeField] private float moveSpeed;
  [SerializeField] private float rotateSpeed;

  [SerializeField] private float weaponDamage;
  [SerializeField] private ParticleSystem muzzleFlash;

  [SerializeField] private PlayerInput playerInput;
  [SerializeField] private InputAction moveAction;
  [SerializeField] private InputAction lookAction;
  [SerializeField] private InputAction attackAction;

  [SerializeField] private Transform camTarget;
  [SerializeField] private Vector2 pitchClampValue;

  [SerializeField] private Animator anim;

  [SerializeField] private Transform debugSphere;

  private float xRotation = 0f;

  void Start()
  {
    Cursor.lockState = CursorLockMode.Locked;

    moveAction = playerInput.actions["Move"];
    lookAction = playerInput.actions["Look"];
    attackAction = playerInput.actions["Attack"];
  }

  void Update()
  {
    Vector2 inputVector = moveAction.ReadValue<Vector2>();

    anim.SetFloat("InputX", inputVector.x);
    anim.SetFloat("InputY", inputVector.y);

    inputVector = inputVector.normalized;

    Vector3 movDir = transform.right * inputVector.x + transform.forward * inputVector.y;
    transform.position += movDir * moveSpeed * Time.deltaTime;

    //Rotate the player
    HandleRotation();
    //transform.forward = Vector3.Slerp(transform.forward, movDir, rotateSpeed * Time.deltaTime);

    Vector2 screenCenter = new Vector2(Screen.width/2f, Screen.height/2f);

    Ray ray = Camera.main.ScreenPointToRay(screenCenter);
    
    if (attackAction.WasPressedThisFrame())
    {
      muzzleFlash.Play();
      if (Physics.Raycast(ray, out RaycastHit hitInfo, 999f))
      {
        debugSphere.position = hitInfo.point;
        Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.red);

        EnemyHealth enemyHealth = hitInfo.collider.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
          enemyHealth.TakeDamage(weaponDamage); 
        }
      }
    }

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

}

