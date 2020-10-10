using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class BulletScript : MonoBehaviour
    {
        // Used to damage enemy
        [SerializeField]
        int damage = 25;


        private void OnCollisionEnter(Collision collision) {

            if (collision.transform.tag == "Enemy") {
                // Debug.Log(collision.transform.name);
                EnemyAI enemy = collision.transform.GetComponent<EnemyAI>();
                enemy.TakeDamage(damage);
                gameObject.SetActive(false);
            }
        }
    }
}