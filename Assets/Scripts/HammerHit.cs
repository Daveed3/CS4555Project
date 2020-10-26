using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Diagnostics;
using Assets.Scripts;

public class HammerHit : MonoBehaviour
{
    public Animator animator;
    public AudioSource hammerHit;

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public int damage = 10;

    // Use this for initialization
    void Start()
    {
        animator = animator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("HammerHit");
            Attack();
            hammerHit.Play();
        }
    }

    // Method for attack damage
    void Attack()
    {
        // Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // Damage enemies
        foreach (Collider enemy in hitEnemies)
        {
            EnemyAI enemyComp = enemy.transform.GetComponent<EnemyAI>();
            enemyComp.TakeDamage(damage);
        }


    }

    // Debug attack radius
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
