using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject playerBody;
    public Animator animator;

    public float originalPlayerHeight = 3.8f;
    public float crouchedHeight = 2.0f;
    public float proneHeight = 0.75f;

    public float movementSpeed = 5f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;

    private bool isGrounded;
    private bool isCrouched = false;
    private bool isProne = false;

    public Inventory Inventory;

    public HUD Hud;

    private IInventoryItem itemToPickup = null;
    public GameObject Hand;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && itemToPickup != null)
        {
            //Inventory.AddItem(itemToPickup);
            //itemToPickup.OnPickup();
            GameObject inventoryItem = (itemToPickup as MonoBehaviour).gameObject;
            inventoryItem.transform.parent = Hand.transform;
            inventoryItem.transform.localPosition = (itemToPickup as InventoryItem).PickupPosition;
            inventoryItem.transform.localEulerAngles = (itemToPickup as InventoryItem).PickupRotation;

            Hud.CloseMessagePanel();
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
       
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            animator.SetInteger("condition", 1);
        }
        else if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetInteger("condition", 0);
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


    /*private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        IInventoryItem item = hit.collider.GetComponent<IInventoryItem>();
        if(item != null)
        {
            Inventory.AddItem(item);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if (item != null)
        {
            itemToPickup = item;
            Hud.OpenMessagePanel("");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if (item != null)
        {
            Hud.CloseMessagePanel();
            itemToPickup = null;
        }
    }

    /*private void InventoryItemUsed(object sender, InventoryEventArgs e)
    {
        IInventoryItem item = e.Item;

        GameObject inventoryItem = (item as MonoBehaviour).gameObject;
        inventoryItem.SetActive(true);

        inventoryItem.transform.localPosition = (item as InventoryItem).PickupPosition;
        inventoryItem.transform.localEulerAngles = (item as InventoryItem).PickupRotation;

    }*/



}
