using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyGenerator : MonoBehaviour
    {
        public GameObject enemyType1;
        public GameObject enemyType2;
        public GameObject enemyType3;
        public GameObject parent;

        public int enemyCount;
        public int spawnLimit;
        public int enemyHealth;
        public float spawnTime = 8f;
        public bool spawnEnemies = false;
        static System.Random random = new System.Random();

        public List<GameObject> spawnLocations;

        // list of enemy spawn positions located around the map
        List<EnemySpawnLocation> enemySpawns = new List<EnemySpawnLocation>();

        List<GameObject> enemies = new List<GameObject>();

        void Awake() {
            foreach (Transform spawnLocation in GameObject.Find("EnemySpawner").transform) {
                enemySpawns.Add(new EnemySpawnLocation(spawnLocation.position.x, spawnLocation.position.y, spawnLocation.position.z));
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // add all possible enemy types to a list
            enemies.Add(enemyType1);
            enemies.Add(enemyType2);
            enemies.Add(enemyType3);

            StartCoroutine(SpawnEnemy());
        }

        IEnumerator SpawnEnemy()
        {
            while (true)
            {
                if (spawnEnemies)
                {
                    EnemySpawnLocation spawnPosition;
                    if (RoundManager.Round < 4)
                    {
                        // there are 4 in-house alien spawns (located in the last 4 positions in the list) 
                        // before round 4, only spawn aliens outside
                        spawnPosition = enemySpawns[random.Next(enemySpawns.Count - 4)];

                    }
                    else
                    {
                        // on and after round 4 start spawning aliens indoor
                        spawnPosition = enemySpawns[random.Next(enemySpawns.Count)];
                    }
                    // Debug.Log(spawnPosition.X + " " + spawnPosition.Y + " " + spawnPosition.Z);
                    GameObject newEnemy = Instantiate(enemies[random.Next(enemies.Count)]);
                    EnemyAI enemy = newEnemy.GetComponent<EnemyAI>();
                    enemy.health = enemyHealth;

                    newEnemy.transform.SetParent(parent.transform);
                    newEnemy.transform.position = new Vector3(spawnPosition.X, spawnPosition.Y, spawnPosition.Z);
                    newEnemy.transform.rotation = Quaternion.identity;
                    newEnemy.SetActive(true);
                    Animator animator = newEnemy.GetComponent<Animator>();
                    animator.SetInteger("HasSpawned", 1);
                    enemyCount += 1;
                    if (enemyCount >= spawnLimit)
                    {
                        spawnEnemies = false;
                    }
                    // Debug.Log("enemy count" + enemyCount);
                    yield return new WaitForSeconds(spawnTime);
                }
                else
                {
                    yield return null;
                }
            }            
        }
    }
}