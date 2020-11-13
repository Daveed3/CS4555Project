using UnityEngine;
using System.Collections;

namespace Assets.Scripts {

    public class AssaultRifleShoot : MonoBehaviour
    {
        public AssaultRifle assaultRifle;
        public float timeBetweenShots;
        public float timeBetweenShooting;
        public float spread;
        public float range;

        public bool shooting;

        public Camera playerCamera;
        public Transform attackPoint;
        public RaycastHit rayHit;
        public LayerMask enemy;

        public GameObject muzzleFlash;
        public GameObject bulletHoleGraphic;
        public AudioSource shootingSFX;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Shoot();
            }
            else if(shootingSFX.isPlaying)
            {
                GetComponent<Animator>().SetBool("Fire", false);
                shootingSFX.Stop();
            }
        }

        public void Shoot()
        {
            if (assaultRifle.HasAmmunition)
            {
                float x = Random.Range(-spread, spread);
                float y = Random.Range(-spread, spread);

                Vector3 direction = playerCamera.transform.forward + new Vector3(x, y, 0);

                if (!shootingSFX.isPlaying)
                {
                    shootingSFX.Play();
                }

                assaultRifle.Shoot();
                if (Physics.Raycast(playerCamera.transform.position, direction, out rayHit, range, enemy))
                {
                    Debug.Log("Hit something" + rayHit.collider.name);

                    if (rayHit.collider.transform.tag == "Enemy")
                    {
                        EnemyAI enemy = rayHit.collider.transform.GetComponent<EnemyAI>();
                        enemy.TakeDamage(assaultRifle.Damage);

                    }
                }

                GameObject tempFlash = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
                Destroy(tempFlash, 0.5f);

                GetComponent<Animator>().SetBool("Fire", true);
            }
           
        }
    }
}
