using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class EnemyAI : MonoBehaviour
    {
        public float speed = 0.5f;
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
            target = GameObject.Find(target.name).transform;
            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;
        }

        private void ChaseTarget()
        {
            agent.SetDestination(target.position);
            animator.SetInteger("IsWalking", 1);
            animator.SetInteger("IsIdle", 0);
        }

        private void AttackTarget()
        {
            //Make sure enemy doesn't move while attacking
            agent.SetDestination(transform.position);

            transform.LookAt(target);

            if (!hasAttacked)
            {
                //Attack code here (i.e. scratches)
                AudioSource attackSound = attackSounds[random.Next(attackSounds.Count)];

                attackSound.Play();

                Debug.Log("Attacked");
                animator.SetInteger("EnemyHasAttacked", 1);
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
            animator.SetInteger("EnemyHasAttacked", 0);
            hasAttacked = false;
        }

        public void TakeDamage(int damage)
        {
            Debug.Log("Alien took damage");
            health -= damage;
            player.IncreaseScore(true);

            if (health <= 0 && !isDead) {
                isDead = true;
                deadEnemyCount += 1;
                health = 0;
                Debug.Log("alien is dead");
                player.IncreaseKillCount();
                animator.SetInteger("IsWalking", 0);
                animator.SetInteger("IsDead", 1);
                Invoke(nameof(DestroyEnemy), 10f);
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