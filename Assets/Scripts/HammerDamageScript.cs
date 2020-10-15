using UnityEngine;

namespace Assets.Scripts
{
    public class HammerDamageScript : MonoBehaviour
    {
        // Used to damage enemy
        [SerializeField]
        int damage = 10;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Enemy")
            {
                // Debug.Log(collision.transform.name);
                EnemyAI enemy = collision.transform.GetComponent<EnemyAI>();
                enemy.TakeDamage(damage);
            }
        }
    }
}