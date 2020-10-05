using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public LayerMask groundMask, targetMask;

    //Attacking
    public float timeBetweenAttacks;
    bool hasAttacked;

    //States
    public float attackRange;
    public bool isInAttackRange;
    public int health;

    private void Awake() {
        target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void ChaseTarget() {
        agent.SetDestination(target.position);
    }

    private void AttackTarget() {
        //Make sure enemy doesn't move while attacking
        agent.SetDestination(transform.position);

        transform.LookAt(target);

        if (!hasAttacked) {
            //Attack code here (i.e. scratches)
            Debug.Log("Attacked");

            hasAttacked = true;
            Invoke(nameof(ResetAttact), timeBetweenAttacks);
        }
    }

    private void ResetAttact() {
        hasAttacked = false;
    }

    public void TakeDamage(int damage) {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 5f);
    }

    private void DestroyEnemy() {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        isInAttackRange = Physics.CheckSphere(transform.position, attackRange, targetMask);

        ChaseTarget();

        if (isInAttackRange) {
            AttackTarget();
        } 
    }


}
