using UnityEngine;
using System.Collections;

namespace Assets.Scripts {

    public class HandgunShoot : MonoBehaviour
    {
        public Handgun handgun;
        public float timeBetweenShots;
        public float nextFireTime;
        public float spread;
        public float range;

        public bool shooting;

        public Camera playerCamera;
        public Transform attackPoint;
        public RaycastHit rayHit;
        public LayerMask enemy;

        public GameObject muzzleFlash;
        public GameObject bulletHoleGraphic;

        public GameObject bulletPrefab;
        public GameObject casingPrefab;
        public GameObject muzzleFlashPrefab;
        public Transform barrelLocation;
        public Transform casingExitLocation;
        public Animator armAnimator;
        public AudioSource shootingSFX;
        public AudioSource emptyGunshot;

        void Start()
        {
            armAnimator = armAnimator.GetComponent<Animator>();

            if (barrelLocation == null)
            {
                barrelLocation = transform;
            }
        }

        private void Update()
        {           
            Shoot();           
        }

        public void Shoot()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (handgun.HasAmmunition && Time.time > nextFireTime)
                {
                    nextFireTime = Time.time + timeBetweenShots;
                    float x = Random.Range(-spread, spread);
                    float y = Random.Range(-spread, spread);

                    Vector3 direction = playerCamera.transform.forward + new Vector3(x, y, 0);


                    armAnimator.SetTrigger("ShootHandgun");
                    shootingSFX.Play();
                    handgun.Shoot();

                    if (Physics.Raycast(playerCamera.transform.position, direction, out rayHit, range, enemy))
                    {
                        Debug.Log("Hit something" + rayHit.collider.name);

                        if (rayHit.collider.transform.tag == "Enemy")
                        {
                            EnemyAI enemy = rayHit.collider.transform.GetComponent<EnemyAI>();
                            enemy.TakeDamage(handgun.Damage);
                        }
                    }

                    GameObject tempFlash = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
                    Destroy(tempFlash, 0.5f);

                    GetComponent<Animator>().SetTrigger("Fire");
                }
                else if(!handgun.HasAmmunition && Time.time > nextFireTime)
                {
                    if (!emptyGunshot.isPlaying)
                    {
                        emptyGunshot.Play();
                    }
                }
                Invoke("Shoot", timeBetweenShots);
            }
        }
    }
}
