using UnityEngine;

public class PersonalMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Animator Animator;
    public Transform cameraTransform;

    public bool kek = false;
    
    public float speed = 6f;

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            Vector3 movementVector = forward * vertical + right * horizontal;
            movementVector.Normalize();
            movementVector *= speed * Time.fixedDeltaTime;
            movementVector.y = rb.velocity.y;
            rb.velocity = movementVector;
            
            Animator.SetFloat("velocity", movementVector.magnitude);
        }
        else
        {
            Animator.SetFloat("velocity", 0);
        }
    }
}