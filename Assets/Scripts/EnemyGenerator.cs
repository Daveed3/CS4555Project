﻿using System;
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
        // will need to change these once we have the environment set
        List<EnemySpawnLocation> enemySpawns = new List<EnemySpawnLocation>()
    {
        
        // new EnemySpawnLocation(38.21f, 8.827f, -24.7f),
        // new EnemySpawnLocation(69.21f, 8.827f, -17.46f),
        // new EnemySpawnLocation(56.58f, 8.827f, -5.27f),
        // new EnemySpawnLocation(43.1f, 8.827f, 27f),
        // new EnemySpawnLocation(58.3f, 8.827f, 19.66f),
        // new EnemySpawnLocation(27.4f, 8.827f, -2.5f),
        // new EnemySpawnLocation(32.1f, 8.827f, -45.1f),
        // new EnemySpawnLocation(94.72f, 8.827f, -16.8f),
        // new EnemySpawnLocation(86.62f, 8.827f, 16.63f),
        // new EnemySpawnLocation(66.2f, 8.827f, 28.08f)
    };

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
                    EnemySpawnLocation spawnPosition = enemySpawns[random.Next(enemySpawns.Count)];
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