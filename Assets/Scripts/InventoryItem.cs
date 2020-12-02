using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class InventoryItem : MonoBehaviour, IInventoryItem
    {
        public Vector3 PickupPosition;
        public Vector3 PickupRotation;
        public GameObject parent;
        public string ItemName;

        public Sprite spriteImage;

        public Sprite Image
        {
            get
            {
                return spriteImage;
            }

            set
            {
                spriteImage = value;
            }
        }

        public string Name
        {
            get
            {
                return ItemName;
            }
            set
            {
                ItemName = value;
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual void OnUse()
        {
            gameObject.SetActive(true);
            transform.parent = parent.transform;
            transform.localPosition = PickupPosition;
            transform.localEulerAngles = PickupRotation;
        }

        public virtual void OnPutAway()
        {
            gameObject.SetActive(false);
        }

        public virtual void OnPickup()
        {
            // turn off the last light after picking up item
            Light[] lights = gameObject.GetComponentsInChildren<Light>();
            lights[lights.Length - 1].enabled = false;

            gameObject.SetActive(false);
        }
    }
}