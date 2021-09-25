using UnityEngine;

public class PersonalMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Animator Animator;
    public Transform cameraTransform;
    
    public float speed = 6f;

    // Update is called once per frame
    void Update()
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
            movementVector *= speed * Time.deltaTime;
            Animator.SetFloat("velocity", movementVector.magnitude);

            movementVector.y = rb.velocity.y;
            rb.velocity = movementVector;
        }
        else
        {
            Animator.SetFloat("velocity", 0);
        }
    }
}