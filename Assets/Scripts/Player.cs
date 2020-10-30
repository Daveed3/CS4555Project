using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public int Health = 100;
        public bool isDead = false;
        public InventoryManager InventoryManager;
        InventoryItem EquippedItem
        {
            get
            {
                return EquippedItem;
            }
            set
            {
                EquippedItem = InventoryManager.activeItem;
            }
            
        }


        void Start()
        {
            StartCoroutine(RegenerateHealth());
        }

        private IEnumerator RegenerateHealth()
        {
            while (!isDead)
            {
                if (Health < 100)
                {
                    Health += 20;
                    yield return new WaitForSeconds(3.5f);
                }
                else
                {
                    yield return null;
                }
            }
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if(Health <= 0)
            {
                isDead = true;
            }
        }
    }
}