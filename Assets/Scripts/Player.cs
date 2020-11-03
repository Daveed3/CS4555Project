using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public int Health = 100;
        public static bool IsDead = false;
        public static int Score = 0;
        public int KillCount = 0;
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
            while (!IsDead)
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

        public void IncreaseScore(bool HitEnemy)
        {
            if(HitEnemy)
            {
                Score += 15;
                Debug.Log($"Score is {Score}");
            }
        }

        public void DecreaseScore(int Amount)
        {
            Score -= Amount;
            Debug.Log($"Score is {Score}");
        }

        public void IncreaseKillCount()
        {
            KillCount += 1;
            Debug.Log($"Kill count is {KillCount}");
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if(Health <= 0)
            {
                IsDead = true;
            }
        }
    }
}