using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Hosting;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;
        public GameObject playerBody;
        public Animator armAnimator;
        public Animator placeholderArmAnimator;
        public Animator bodyAnimator;
        public AudioSource walkingMediumSFX;
        public AudioSource walkingFastSFX;

        public GameObject cameraPlayerArms;
        public GameObject placeHolderPlayerArms;

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
        public GameObject Chest;
        public GameObject handgun;

        private HammerHit hammerScript;
        private SimpleShoot handgunScript;

        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponent<CharacterController>();
            armAnimator = armAnimator.GetComponent<Animator>();
            placeholderArmAnimator = placeholderArmAnimator.GetComponent<Animator>();
            bodyAnimator = bodyAnimator.GetComponent<Animator>();

            hammerScript = playerBody.GetComponent<HammerHit>();
            handgunScript = handgun.GetComponent<SimpleShoot>();

            walkingMediumSFX.volume = .4f;
            walkingFastSFX.volume = .4f;
        }

      
        // Update is called once per frame
        void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.F) && itemToPickup != null)
            {
                GameObject inventoryItem = (itemToPickup as MonoBehaviour).gameObject;

                if (itemToPickup.Name.Equals("handgun"))
                {
                    Inventory.AddItem(itemToPickup, 0);                                                
                }
                else if(itemToPickup.Name.Equals("hammer"))
                {
                    Inventory.AddItem(itemToPickup, 1);
                }
                else if(itemToPickup.Name.Equals("flashlight"))
                {
                    Inventory.AddItem(itemToPickup, 2);                
                }

                (itemToPickup as InventoryItem).OnPickup();

                Hud.CloseMessagePanel();
            }

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("walking");
                armAnimator.SetInteger("condition", 1);
                placeholderArmAnimator.SetInteger("condition", 1);
                bodyAnimator.SetInteger("condition", 1);
                walkingMediumSFX.Play();
            }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            {
                Debug.Log("idle");
                armAnimator.SetInteger("condition", 0);
                placeholderArmAnimator.SetInteger("condition", 0);
                bodyAnimator.SetInteger("condition", 0);
                walkingMediumSFX.Stop();
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
                walkingFastSFX.Pause();
                walkingMediumSFX.Pause();
            }


            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.LeftControl) && !isCrouched)
            {
                Crouch();
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouched)
            {
                ResetHeight();
            }

            if (Input.GetKeyDown(KeyCode.Z) && !isProne)
            {
                Prone();
            }
            else if (Input.GetKeyDown(KeyCode.Z) && isProne)
            {
                ResetHeight();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                Run();
                Debug.Log("running");
                armAnimator.SetInteger("IsRunning", 1);
                placeholderArmAnimator.SetInteger("IsRunning", 1);
                bodyAnimator.SetInteger("IsRunning", 1);
                walkingFastSFX.Play();
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift)){
                movementSpeed = movementSpeed / 2;
                Debug.Log("not running");
                armAnimator.SetInteger("IsRunning", 0);
                placeholderArmAnimator.SetInteger("IsRunning", 0);
                bodyAnimator.SetInteger("IsRunning", 0);
                walkingFastSFX.Pause();
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

            if (isCrouched)
            {
                playerBody.transform.localScale += new Vector3(0, 0.52f, 0);
                isCrouched = false;
            }
            else if (isProne)
            {
                playerBody.transform.localScale += new Vector3(0, 0.85f, 0);
                playerBody.transform.position += new Vector3(0, 0.85f, 0);
                // playerBody.transform.localScale -= new Vector3(0, 0, 1.2f);
                isProne = false;
            }
        }

        // run speed
        void Run() {
            movementSpeed = movementSpeed * 2 ;
        }



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


    }
}