using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour {
    List<Rigidbody> rigidbodies = new List<Rigidbody>();  // Ragdoll Rigidbodies
    CharacterController controller = null;
    Animator animator = null;

    public float speed = 80.0f;                           // Movement Speed
    public float pushPower = 2.0f;                        // Power used to push other dynamic objects
    public float yVelocity = 0;                           // Used for jumping and applying gravity
    public float jumpPower = 1.0f;                        // Jump power modifer

    public bool crouch = false;                           // Is the user crouching?
    public bool grounded;                                 // Is the user grounded?

    public bool Ragdoll                                   // Get or Set the user being a ragdoll
    {
        get { return !animator.enabled; }
        set
        {
            animator.enabled = !value;
            foreach (var r in rigidbodies)
                r.isKinematic = !value;
        }
    }


	// Use this for initialization
	void Start () {
        // Add rigidbodies on object into list
        rigidbodies.AddRange(GetComponentsInChildren<Rigidbody>());
        foreach (var r in rigidbodies)
            r.isKinematic = true; // Set all rigidbodies to kinematic

        // Get Controller and Animator components
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        // Get vertical and horizontal movement
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        // Move the player
        //controller.Move(transform.up * yVelocity * jumpPower * Time.deltaTime);
        //controller.Move(transform.forward * vertical * Time.deltaTime);
        controller.Move(((transform.up * yVelocity * jumpPower) + (transform.forward * vertical)) * Time.deltaTime);

        RaycastHit hit;

        // Check if player is on the ground
        grounded = Physics.SphereCast(transform.position + controller.radius * Vector3.up, controller.radius, -Vector3.up, out hit, 0.1f);

        yVelocity += Physics.gravity.y * Time.deltaTime;
        if (grounded && yVelocity < 0)
            yVelocity = 0;

        if (!Ragdoll && Input.GetKeyDown(KeyCode.Space) && grounded)
            yVelocity = 5.0f;

        if (Input.GetKey(KeyCode.R))
            Ragdoll = !Ragdoll;

        crouch = Input.GetKey(KeyCode.C);
        // Make player crouch if their head is under an object
        if (Physics.Raycast(controller.transform.position + controller.height * Vector3.up, Vector3.up, 0.2f))
            crouch = true;

        controller.height = crouch ? 1.1f : 1.7f;
        controller.center = new Vector3(0, controller.height * 0.5f, 0);

        //controller.SimpleMove(transform.forward * vertical * speed * Time.deltaTime);
        //controller.Move(transform.up * Time.deltaTime);
        transform.Rotate(transform.up, horizontal * speed * Time.deltaTime);
        animator.SetFloat("Speed", vertical * speed * Time.deltaTime);
        animator.SetBool("Crouching", crouch);
        animator.SetBool("Jumping", yVelocity > 0);
	}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic) return;

        if (hit.moveDirection.y < -0.3f) return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
    }
}
