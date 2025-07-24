using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    
    [Header("Animation")]
    public Animator animator;
    
    private Rigidbody rb;
    private Vector3 movement;
    private bool isMoving;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (animator == null)
            animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q) ? -1 : 1);
        float vertical = Input.GetAxis("Vertical") * (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z) ? 1 : 1);
        
        movement = new Vector3(horizontal, 0f, vertical).normalized;
        isMoving = movement.magnitude > 0.1f;
        
        if (animator != null)
        {
            animator.SetBool("IsMoving", isMoving);
        }
        
        if (isMoving)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }
    
    void FixedUpdate()
    {
        // Déplacement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}