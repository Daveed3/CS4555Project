using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class RoundManager : MonoBehaviour
    {
        public static int Round = 0;
        private bool StartRounds = false;
        public Player Player;
        public EnemyAI EnemyAI;
        public EnemyGenerator EnemyGenerator;

        
        // Use this for initialization
        void Start()
        {
            // give the player 60 seconds to explore before starting the rounds          
            StartCoroutine(Rounds());
        }

        // Update is called once per frame
        IEnumerator Rounds()
        {
            while (!Player.IsDead)
            {
                if (!StartRounds)
                {
                    // give the player 60 seconds to explore before starting the rounds
                    Debug.Log("Game is starting, wait 10 seconds...");
                    yield return new WaitForSeconds(10);
                    StartRounds = true;
                }
                else if (StartRounds && Round == 0)
                {
                    Round = 1;
                    Debug.Log($"It is now round {Round}");
                    yield return new WaitForSeconds(5);
                    EnemyGenerator.spawnLimit = 5;
                    EnemyGenerator.spawnEnemies = true;
                }
                else if (StartRounds && EnemyAI.deadEnemyCount == EnemyGenerator.enemyCount && !EnemyGenerator.spawnEnemies)
                {
                    Round += 1;
                    Debug.Log($"It is now round {Round}");
                    Debug.Log($"Dead enemy count {EnemyAI.deadEnemyCount}, total enemies allowed is {EnemyGenerator.enemyCount} - going to start a new round");

                    yield return new WaitForSeconds(5);

                    EnemyAI.deadEnemyCount = 0;               
                    EnemyGenerator.enemyCount = 0;
                    EnemyAI.health += 10;
                    EnemyGenerator.spawnLimit += 5;
                    EnemyGenerator.spawnEnemies = true;
                }
                else
                {
                    yield return null;
                }
            }

            Debug.Log("PLAYER IS DEAD!");
        }
    }
}
