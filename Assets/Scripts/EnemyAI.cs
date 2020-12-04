using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class EnemyAI : MonoBehaviour
    {
        public static float speed = 0.8f;
        public NavMeshAgent agent;
        public Transform target;
        public LayerMask groundMask, targetMask;
        Animator animator;
        public Player player;
        private BuildableItem window;

        //Attacking
        public float timeBetweenAttacks = 0.5f;
        public bool hasAttacked;

        //States
        public float attackRange = 1f;
        public bool isInAttackRange;
        public int health;

        private bool isDead = false;
        bool hasSpawned = false;

        public static int deadEnemyCount;


        static System.Random random = new System.Random();
        // audio
        public List<AudioSource> attackSounds;
        public List<AudioSource> normalSounds;
        public AudioSource alienSound;
        public List<AudioSource> hitBarrierSounds;
        public AudioSource gunDamageSound;
        public AudioSource hammerDamageSound;

        private const int NUM_OF_POSSIBLE_ATTACKS = 8;
        private const int NUM_OF_POSSIBLE_DEATHS = 3;
        private const int NUM_OF_POSSIBLE_RUNS = 2;

        private int runAnimationNumber;


        public AudioSource walkSFX;
        public AudioSource runSFX;
        IEnumerator Start()
        {
            animator = GetComponent<Animator>();
            animator.SetInteger("HasSpawned", 1);
            animator.SetInteger("IsIdle", 1);
           
            yield return new WaitForSeconds(3);
            hasSpawned = true;
        }

        private void Awake()
        {
            runAnimationNumber = GetRandomAction(NUM_OF_POSSIBLE_RUNS);

            target = GameObject.Find(target.name).transform;
            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;
            Debug.Log($"agent speed is {speed}");
        }

        private void ChaseTarget()
        {
            agent.SetDestination(target.position);
            if (speed > 2.5f)
            {
                if (!runSFX.isPlaying)
                {
                    runSFX.Play();
                }
                animator.SetInteger($"IsRunning_{runAnimationNumber}", 1);
            }
            else
            {
                if (!walkSFX.isPlaying)
                {
                    walkSFX.Play();
                }
                animator.SetInteger("IsWalking", 1);
            }

            animator.SetInteger("IsIdle", 0);
        }

        private void AttackTarget()
        {
            //Make sure enemy doesn't move while attacking
            agent.SetDestination(transform.position);
            walkSFX.Stop();
            runSFX.Stop();

            transform.LookAt(target);

            if (!hasAttacked)
            {
                //Attack code here (i.e. scratches)
                AudioSource attackSound = attackSounds[random.Next(attackSounds.Count)];

                attackSound.Play();

                Debug.Log("Attacked");
                animator.SetTrigger($"EnemyHasAttacked_{GetRandomAction(NUM_OF_POSSIBLE_ATTACKS)}");
                
                hasAttacked = true;


                if (target.name == "Player")
                {
                    player.TakeDamage(20);
                }
                else if (target.name.Contains("wood planks"))
                {
                    if (window != null)
                    {
                        window.OnDamaged(20);
                        hitBarrierSounds[random.Next(hitBarrierSounds.Count)].Play();
                        if (window.Health <= 0)
                        {
                            transform.position = target.position;
                            target = player.transform;
                            targetMask = LayerMask.GetMask("Player");
                            agent.autoTraverseOffMeshLink = true;
                            animator.SetTrigger("Jump");
                            window = null;                           
                        }
                    }
                }

                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

        private void ResetAttack()
        {
            hasAttacked = false;
        }

        public void TakeDamage(int damage)
        {
            Debug.Log("Alien took damage");

            if (health <= 0 && !isDead) {
                //Make sure enemy stops moving after dying
                agent.SetDestination(transform.position);
                walkSFX.Stop();
                runSFX.Stop();
                alienSound.Stop();

                isDead = true;
                deadEnemyCount += 1;
                health = 0;
                Debug.Log("alien is dead");
                player.IncreaseKillCount();
                animator.SetInteger("IsWalking", 0);
                animator.SetInteger($"IsRunning_{runAnimationNumber}", 0);
                gameObject.GetComponent<BoxCollider>().enabled = false; // turn off alien BoxColliders when they die
                gameObject.GetComponent<NavMeshAgent>().enabled = false; // turn off alien NavMeshAgent when they die

                animator.SetInteger($"IsDead_{GetRandomAction(NUM_OF_POSSIBLE_DEATHS)}", 1);
                Invoke(nameof(DestroyEnemy), 10f);
            } else if (health > 0) {
                health -= damage;

                if (player.EquippedItem.ItemName.Equals("handgun") || player.EquippedItem.ItemName.Equals("assault rifle")) {
                    gunDamageSound.Play();
                }
                else
                {
                    hammerDamageSound.Play();
                }

                player.IncreaseScore(HitEnemy: true, BuiltBarrier: false);
            }
        }

        private void DestroyEnemy()
        {
            Destroy(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            if (!isDead && hasSpawned)
            {
                isInAttackRange = Physics.CheckSphere(transform.position, attackRange, targetMask);

                ChaseTarget();

                // 1/20 chance of making a sound
                if (Random.Range(1, 21) == 3)
                {
                    if (!alienSound.isPlaying)
                    {
                        alienSound = normalSounds[Random.Range(0, normalSounds.Count)];
                        alienSound.Play();

                    }
                }

                if (isInAttackRange)
                {
                    AttackTarget();
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Starting OnTrigger");
            IBuildableItem buildableItem = other.GetComponent<IBuildableItem>();
            
            if(buildableItem != null)
            {
                if ((buildableItem as BuildableItem).Health > 0)
                {
                    window = buildableItem as BuildableItem;
                    target = window.transform;
                    targetMask = LayerMask.GetMask("Window");
                    agent.autoTraverseOffMeshLink = false;
                    Debug.Log("Need to stop");
                }
                else
                {
                    SetMovementSpeed(0.67f);
                    animator.SetTrigger("Jump");
                    agent.autoTraverseOffMeshLink = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            IBuildableItem buildableItem = other.GetComponent<IBuildableItem>();
            if (buildableItem != null)
            {
                // window = null;
                // agent.autoTraverseOffMeshLink = false;
                SetMovementSpeed(speed);
            }
        }

        public void SetMovementSpeed(float speed)
        {
            agent.speed = speed;
        }

        private int GetRandomAction(int max)
        {
            return Random.Range(1, max + 1);
        }

        void OnDrawGizmosSelected()
        {
            if (isInAttackRange == false)
            {
                return;
            }

            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}