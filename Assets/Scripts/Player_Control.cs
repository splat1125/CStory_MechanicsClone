using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public Rigidbody rb;

    public float GroundSpeed = 1f;
    public bool Grounded = true;
    public float RaycastDistance = 1f;


    bool leftInput = false;
    bool rightInput = false;
    bool jumpInput = false;
    bool fallInput = false;

    bool movingLeft = false;
    bool movingRight = false;

    public float JumpForce = 1f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        RegisterInputs();
        DetermineMovement();
        HorizontalMovement();
        if (jumpInput) Jumping();
    }

    void RegisterInputs()
    {
        leftInput = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        rightInput = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        jumpInput = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
        fallInput = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
    }

    void DetermineMovement()
    {
        movingLeft = leftInput && !rightInput;
        movingRight = rightInput && !leftInput;
    }

    void HorizontalMovement()
    {
        if (movingLeft && !WallDetection(Vector3.left))
        {
            if (Grounded) transform.position += new Vector3(-GroundSpeed * Time.deltaTime, 0, 0);
            else transform.position += new Vector3((-GroundSpeed/2) * Time.deltaTime, 0, 0);
        }
        if (movingRight && !WallDetection(Vector3.right))
        {
            if (Grounded) transform.position += new Vector3(GroundSpeed * Time.deltaTime, 0, 0);
            else transform.position += new Vector3((GroundSpeed/2) * Time.deltaTime, 0, 0);
        }
    }

    bool WallDetection(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, RaycastDistance, 255)) return true;
        else return false;
    }

    void Jumping()
    {
        if (Grounded)
        {
            rb.AddForce(0, JumpForce, 0);
            Grounded = false;

        }
    }

    void FixedUpdate()
    {
        if (!Grounded)
        {
            Grounded = WallDetection(Vector3.down);
        }
    }
}
