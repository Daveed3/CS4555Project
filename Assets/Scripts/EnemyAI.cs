using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class EnemyAI : MonoBehaviour
    {
        public NavMeshAgent agent;
        public Transform target;
        public LayerMask groundMask, targetMask;
        Animator animator;
        public Player player;

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
            target = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();
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
                Debug.Log("Attacked");
                animator.SetInteger("EnemyHasAttacked", 1);
                hasAttacked = true;
                player.TakeDamage(20);
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
    }
}