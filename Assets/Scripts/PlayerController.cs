using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Hosting;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public CharacterController controller;
        public GameObject playerBody;
        public Animator armAnimator;
        public Animator placeholderArmAnimator;
        public Animator bodyAnimator;

        public AudioSource ambientSFX;


        public AudioSource walkingOutdoorMediumSFX;
        public AudioSource walkingOutdoorFastSFX;
        public AudioSource walkingIndoorMediumSFX;
        public AudioSource walkingIndoorFastSFX;

        private bool indoor = false;
        private bool outdoor = false;

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
        public Player Player;
        public HUD Hud;

        private IInventoryItem itemToPickup = null;
        private IBuildableItem itemToBuild = null;

        public List<AudioSource> buildingBarrierRemarks;

        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponent<CharacterController>();
            armAnimator = armAnimator.GetComponent<Animator>();
            placeholderArmAnimator = placeholderArmAnimator.GetComponent<Animator>();
            bodyAnimator = bodyAnimator.GetComponent<Animator>();
        }

      
        // Update is called once per frame
        void Update()
        {
            ambientSFX.loop = true;
            if (Physics.Raycast(groundCheck.transform.position, Vector3.down, out RaycastHit hit))
            {
                var floortag = hit.collider.gameObject.tag;
                if (floortag == "Indoor")
                {
                    walkingOutdoorMediumSFX.Stop();
                    walkingOutdoorFastSFX.Stop();
                    outdoor = false;
                    indoor = true;
                }
                else
                {
                    walkingIndoorMediumSFX.Stop();
                    walkingIndoorFastSFX.Stop();
                    outdoor = true;
                    indoor = false;
                }
            }


            if (Input.GetKeyDown(KeyCode.F) && itemToPickup != null)
            {
                if (itemToPickup.Name.Equals("assault rifle"))
                {
                    Inventory.AddItem(itemToPickup, 0);
                }
                else if (itemToPickup.Name.Equals("handgun"))
                {
                    Inventory.AddItem(itemToPickup, 1);
                }
                else if (itemToPickup.Name.Equals("hammer"))
                {
                    Inventory.AddItem(itemToPickup, 2);
                }
                else if (itemToPickup.Name.Equals("flashlight"))
                {
                    Inventory.AddItem(itemToPickup, 3);
                }
                else if (itemToPickup.Name.Equals("building material") && (Inventory.Items[4] == null) && Player.Score >= (itemToPickup as BuildingMaterial).cost)
                {
                    Debug.Log("adding for first time");
                    Inventory.AddItem(itemToPickup, 4);                       
                }
                // do not need to add ammunition to inventory
                // instead, increase the ammo count in the handgun class

                (itemToPickup as InventoryItem).OnPickup();
                itemToPickup = null;
                Hud.CloseMessagePanel();
            }
            else if(Input.GetKeyDown(KeyCode.F) && itemToBuild != null)
            {
                if (itemToBuild.Name.Equals("buildable"))
                {
                    (Player.EquippedItem as Hammer).BuildBarrier();
                    (itemToBuild as BuildableItem).OnRebuild();

                    // 1/5 chance of making a remark when building windows
                    if (Random.Range(1, 6) == 3)
                    {
                        AudioManager.CheckAndPlayAudio(buildingBarrierRemarks[Random.Range(0, buildingBarrierRemarks.Count)]);                   
                    }
                }
                itemToBuild = null;
            }

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
               // Debug.Log("walking");
                armAnimator.SetInteger("IsWalking", 1);
                placeholderArmAnimator.SetInteger("IsWalking", 1);
                bodyAnimator.SetInteger("IsWalking", 1);
                if(indoor)
                {
                    if (!walkingIndoorMediumSFX.isPlaying)
                    {
                        walkingIndoorMediumSFX.Play();
                    }
                }
                else if(outdoor)
                {
                    if (!walkingOutdoorMediumSFX.isPlaying)
                    {
                        walkingOutdoorMediumSFX.Play();
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            {
               // Debug.Log("idle");
                armAnimator.SetInteger("IsWalking", 0);
                placeholderArmAnimator.SetInteger("IsWalking", 0);
                bodyAnimator.SetInteger("IsWalking", 0);
                if (indoor)
                {          
                    walkingIndoorMediumSFX.Stop();                    
                }
                else if (outdoor)
                {
                    walkingOutdoorMediumSFX.Stop();
                }
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
                walkingIndoorMediumSFX.Pause();
                walkingIndoorFastSFX.Pause();
                walkingOutdoorFastSFX.Pause();
                walkingOutdoorMediumSFX.Pause();
            }


            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                Run();
                //Debug.Log("running");
                armAnimator.SetInteger("IsRunning", 1);
                placeholderArmAnimator.SetInteger("IsRunning", 1);
                bodyAnimator.SetInteger("IsRunning", 1);
                if (indoor)
                {
                    walkingIndoorFastSFX.Play();
                }
                else if (outdoor)
                {
                    walkingOutdoorFastSFX.Play();
                }
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift)){
                movementSpeed = movementSpeed / 2;
               // Debug.Log("not running");
                armAnimator.SetInteger("IsRunning", 0);
                placeholderArmAnimator.SetInteger("IsRunning", 0);
                bodyAnimator.SetInteger("IsRunning", 0);
                if (indoor)
                {
                    walkingIndoorFastSFX.Stop();
                }
                else if (outdoor)
                {
                    walkingOutdoorFastSFX.Stop();
                }
            }
        }

        // run speed
        private void Run() {
            movementSpeed = movementSpeed * 2 ;
        }

        private void OnTriggerEnter(Collider other)
        {
            IInventoryItem item = other.GetComponent<IInventoryItem>();
            IBuildableItem buildableItem = other.GetComponent<IBuildableItem>();
            if (item != null)
            {
                itemToPickup = item;

                // display unique message if the item is ammunition and the player does not have enough score to purchase it
                if (itemToPickup.Name.Equals("ammunition") && Player.Score < (itemToPickup as Ammunition).cost)
                {
                    Hud.OpenMessagePanel($"{(itemToPickup as Ammunition).Message}");
                }
                else if (itemToPickup.Name.Equals("building material") && Player.Score < (itemToPickup as BuildingMaterial).cost)
                {
                    Hud.OpenMessagePanel($"{(itemToPickup as BuildingMaterial).Message}");
                }
                else
                {
                    Hud.OpenMessagePanel($"Press F to pick up {item.Name}");
                } 
            }
            else if(buildableItem != null && Player.EquippedItem != null && Player.EquippedItem.ItemName.Equals("hammer") && Inventory.Items[4] != null && (Inventory.Items[4] as BuildingMaterial).MaterialCount > 0)
            {
                if ((buildableItem as BuildableItem).Health < 100)
                {
                    itemToBuild = buildableItem;
                    Hud.OpenMessagePanel("Press F to rebuild barrier");
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            IInventoryItem item = other.GetComponent<IInventoryItem>();
            IBuildableItem buildableItem = other.GetComponent<IBuildableItem>();
            if (item != null)
            {
                Hud.CloseMessagePanel();
                itemToPickup = null;
            }
            else if(buildableItem != null)
            {
                Hud.CloseMessagePanel();
                itemToBuild = null;
            }
        }
    }
}