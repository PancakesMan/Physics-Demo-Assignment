using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour {
    List<Rigidbody> rigidbodies = new List<Rigidbody>();
    CharacterController controller = null;
    Animator animator = null;

    public float speed = 80.0f;
    public float pushPower = 2.0f;

    public float yVelocity = 0;
    public float jumpPower = 1.0f;
    public bool crouch = false;

    public bool grounded;

    public bool Ragdoll
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
        rigidbodies.AddRange(GetComponentsInChildren<Rigidbody>());
        foreach (var r in rigidbodies)
            r.isKinematic = true;

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        controller.Move(transform.up * yVelocity * jumpPower * Time.deltaTime);
        controller.Move(transform.forward * vertical * Time.deltaTime);

        RaycastHit hit;
        grounded = Physics.SphereCast(transform.position + controller.radius * Vector3.up, controller.radius, -Vector3.up, out hit, 0.1f);

        yVelocity += Physics.gravity.y * Time.deltaTime;
        if (grounded && yVelocity < 0)
            yVelocity = 0;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            yVelocity = 5.0f;

        crouch = Input.GetKey(KeyCode.C);
        controller.height = crouch ? 1.3f : 1.8f;
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
