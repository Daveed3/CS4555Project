using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyType1;
    public GameObject enemyType2;
    public GameObject enemyType3;

    public int enemyCount;
    public int spawnLimit = 5;
    public float spawnTime = 5f;
    static System.Random random = new System.Random();

    // list of enemy spawn positions located around the map
    // will need to change these once we have the environment set
    List<EnemySpawnLocation> enemySpawns = new List<EnemySpawnLocation>()
    {
        new EnemySpawnLocation(-30.68f, 0.024f, -1.08f),
        new EnemySpawnLocation(-19.8f, 0.024f, -34.43f),
        new EnemySpawnLocation(6.7f, 0.024f, -26.54f),
        new EnemySpawnLocation(12.81f, 0.024f, -31.97f),
        new EnemySpawnLocation(21.54f, 0.024f, -25.43f),
        new EnemySpawnLocation(41.23f, 0.024f, -14.4f),
        new EnemySpawnLocation(37.34f, 0.024f, 3.74f),
        new EnemySpawnLocation(41.75f, 0.024f, 14.78f),
        new EnemySpawnLocation(5.9f, 0.024f, 34.5f),
        new EnemySpawnLocation(-19.5f, 0.024f, 23.8f)
    };

    List<GameObject> enemies = new List<GameObject>();

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
        while(enemyCount < spawnLimit)
        {
            EnemySpawnLocation spawnPosition = enemySpawns[random.Next(enemySpawns.Count)];
            Debug.Log(spawnPosition.X + " " + spawnPosition.Y + " " + spawnPosition.Z);
            GameObject newEnemy = Instantiate(enemies[random.Next(enemies.Count)]);
            newEnemy.transform.position = new Vector3(spawnPosition.X, spawnPosition.Y, spawnPosition.Z); // TODO: Need to figure out why position coords are displayed as *10^-1
            newEnemy.transform.rotation = Quaternion.identity;
            newEnemy.SetActive(true);
            Animator animator = newEnemy.GetComponent<Animator>();
            animator.SetInteger("HasSpawned", 1);
            enemyCount += 1;
            Debug.Log("enemy count" + enemyCount);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
