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

        //Attacking
        public float timeBetweenAttacks;
        bool hasAttacked;

        //States
        public float attackRange;
        public bool isInAttackRange;
        public int health;

        public GameObject childMesh;
        public Material[] material;
        Renderer rend;

        private bool isDead = false;
        bool hasSpawned = false;

        IEnumerator Start()
        {
            animator = GetComponent<Animator>();
            animator.SetInteger("HasSpawned", 1);
            animator.SetInteger("IsIdle", 1);
            rend = childMesh.GetComponent<Renderer>();
            rend.enabled = true;
            rend.material = material[0];
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
                // Debug.Log("Attacked");
                //animator.SetInteger("EnemyHasAttacked", 1);
                hasAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
            else
            {
                //animator.SetInteger("EnemyHasAttacked", 0);
            }
        }

        private void ResetAttack()
        {
            hasAttacked = false;
        }

        public void TakeDamage(int damage)
        {
            Debug.Log("Alien took damage");
            health -= damage;
            rend.material = material[1];

            if (health <= 0) {
                isDead = true;
                health = 0;
                Debug.Log("alien is dead");
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