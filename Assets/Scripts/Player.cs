using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public int Health = 100;
        public static bool IsDead = false;
        public static int Score = 0;
        public int KillCount = 0;
        public InventoryManager InventoryManager;

        public List<AudioSource> gruntingSounds;
        public AudioSource gruntSound;

        public List<AudioSource> playerDamagedSounds;
        public AudioSource playerDamagedSound;
        public List<AudioSource> playerRemarksOnAlien;

        public InventoryItem EquippedItem
        {
            get
            {
                return InventoryManager.activeItem;
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

        public void IncreaseScore(bool HitEnemy = false, bool BuiltBarrier = false)
        {
            if(HitEnemy)
            {
                Score += 15;
                Debug.Log($"Score is {Score}");
            }
            else if(BuiltBarrier)
            {
                // rebuilt a barrier
                Score += 10;
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

            // have the player say some random remark about the aliens/house every 8 kills
            if (KillCount % 8 == 0)
            {
                GetRandomPlayerRemarkOnAliens().Play();
            }

            Debug.Log($"Kill count is {KillCount}");
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;

            if(!gruntSound.isPlaying)
            {
                gruntSound = GetRandomGrunt();
                gruntSound.PlayDelayed(1.5f);
            }

            if(!playerDamagedSound.isPlaying)
            {
                playerDamagedSound = GetRandomDamagedSound();
                playerDamagedSound.PlayDelayed(1f);
            }

            if(Health <= 0)
            {
                IsDead = true;
            }
        }

        private AudioSource GetRandomGrunt()
        {
            return gruntingSounds[Random.Range(0, gruntingSounds.Count)];
        }

        private AudioSource GetRandomDamagedSound()
        {
            return playerDamagedSounds[Random.Range(0, playerDamagedSounds.Count)];
        }

        private AudioSource GetRandomPlayerRemarkOnAliens()
        {
            return playerRemarksOnAlien[Random.Range(0, playerRemarksOnAlien.Count)];
        }
    }
}