using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class PlayerLook : MonoBehaviour
    {
        public Camera Cam;
        public Vector3 Offset;
        private Vector3 target;
        public GameObject chest;
        private Transform chestTransform;
        private Animator animator;
    

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            chestTransform = chest.GetComponent<Transform>();
        }

        void LateUpdate()
        {
            target = Cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
            chestTransform.LookAt(target);
            chestTransform.rotation = chestTransform.rotation * Quaternion.Euler(Offset);
        }
    }
}
