using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forward_speed;
    public float back_speed;
    public float side_speed;
    public float jump_speed;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVeolocity;

    public Rigidbody rb;
    public Transform cameraT;

    bool forward_pressed = false;
    bool back_pressed = false;
    bool left_pressed = false;
    bool right_pressed = false;
    bool jump_pressed = false;


    // Start is called before the first frame update
    void Start()
    {
        cameraT = GetComponent<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        forward_pressed = Input.GetKey(KeyCode.W);
        back_pressed = Input.GetKey(KeyCode.S);
        left_pressed = Input.GetKey(KeyCode.A);
        right_pressed = Input.GetKey(KeyCode.D);
        jump_pressed = Input.GetKey(KeyCode.Space);


        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 input_dir = input.normalized;
        //Debug.Log(input_dir);
        Vector2 forward = new Vector2(0.0f, 1.0f);

        if (input_dir == forward)
        {
            float target_rotation = Mathf.Atan2(input_dir.x, input_dir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector2.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, target_rotation, ref turnSmoothVeolocity, turnSmoothTime);
        }
    }

    void FixedUpdate()
    {
        // Phisic code HERE
        move();
    }
    
    private void move()
    {
        if (forward_pressed)
        {
            rb.MovePosition(transform.position + transform.forward * forward_speed * Time.deltaTime);
        }
        if (left_pressed)
        {
            rb.MovePosition(transform.position + -transform.right * side_speed * Time.deltaTime);
        }
        if (right_pressed)
        {
            rb.MovePosition(transform.position + transform.right * side_speed * Time.deltaTime);
        }
        if (back_pressed)
        {
            rb.MovePosition(transform.position + -transform.forward * back_speed * Time.deltaTime);
        }
        if (jump_pressed && isGrounded())
        {
            rb.AddForce(transform.up * jump_speed * Time.deltaTime);
        }
    }

    private bool isGrounded()
    {
        float DisstanceToTheGround = GetComponent<Collider>().bounds.extents.y;
        return Physics.Raycast(transform.position, Vector3.down, DisstanceToTheGround + 0.1f);
    }
}
