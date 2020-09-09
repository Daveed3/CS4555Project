using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject playerBody;

    public float originalPlayerHeight = 3.8f;
    public float crouchedHeight = 2.0f;
    public float proneHeight = 0.75f;

    public float movementSpeed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;

    private bool isGrounded;
    private bool isCrouched = false;
    private bool isProne = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = controller.transform.right * x + controller.transform.forward * z;

        // frame rate independent
        controller.Move(move * movementSpeed * Time.deltaTime);

        // jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.LeftControl) && !isCrouched)
        {
            Crouch();
        }
        else if(Input.GetKeyDown(KeyCode.LeftControl) && isCrouched)
        {
            ResetHeight();
        }

        if(Input.GetKeyDown(KeyCode.Z) && !isProne)
        {
            Prone();
        }
        else if(Input.GetKeyDown(KeyCode.Z) && isProne)
        {
            ResetHeight();
        }
    }

    // reduce height to crouching height
    void Crouch()
    {
        controller.height = crouchedHeight;
        playerBody.transform.localScale -= new Vector3(0, 0.52f, 0);
        isCrouched = true;
    }

    // reduce height to crawling height
    void Prone()
    {
        controller.height = proneHeight;
        playerBody.transform.localScale -= new Vector3(0, 0.85f, 0);
        playerBody.transform.position -= new Vector3(0, 0.85f, 0);

        //  playerBody.transform.localScale += new Vector3(0, 0, 1.2f);
        isProne = true;
    }

    // reset height
    void ResetHeight()
    {
        controller.height = originalPlayerHeight;

        if(isCrouched)
        {
            playerBody.transform.localScale += new Vector3(0, 0.52f, 0);
            isCrouched = false;
        }
        else if(isProne)
        {
            playerBody.transform.localScale += new Vector3(0, 0.85f, 0);
            playerBody.transform.position += new Vector3(0, 0.85f, 0);
            // playerBody.transform.localScale -= new Vector3(0, 0, 1.2f);
            isProne = false;
        }
    }
}
