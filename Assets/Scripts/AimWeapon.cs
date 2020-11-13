using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class AimWeapon : MonoBehaviour
    {
        public Player player;
        public Camera playerCamera;
        private Vector3 originalPosition;
        public Vector3 handgunAimPosition;
        public Vector3 assaultRifleAimPosition;
        private float handgunAimSpeed = 6f;
        private float assaultRifleAimSpeed = 4f;

        // Use this for initialization
        void Start()
        {
            originalPosition = transform.localPosition;

        }

        // Update is called once per frame
        void Update()
        {
            AimDownSights();
        }

        private void AimDownSights()
        {
            if (Input.GetButton("Fire2") && player.EquippedItem != null && player.EquippedItem.Name.Equals("handgun"))
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, handgunAimPosition, Time.deltaTime * handgunAimSpeed);
                playerCamera.fieldOfView = 40;
            }
            else if(Input.GetButton("Fire2") && player.EquippedItem != null && player.EquippedItem.Name.Equals("assault rifle")) {
                transform.localPosition = Vector3.Lerp(transform.localPosition, assaultRifleAimPosition, Time.deltaTime * assaultRifleAimSpeed);
                playerCamera.fieldOfView = 40;
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * handgunAimSpeed);
                playerCamera.fieldOfView = 60;
            }
        }
    }
}