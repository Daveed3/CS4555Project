using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy;
    public int enemyCount;
    public int spawnLimit = 5;
    public float spawnTime = 5f;
    static System.Random random = new System.Random();
    public GameObject Environment;

    // list of enemy spawn positions located around the map
    // will need to change these once we have the environment set
    List<EnemySpawnLocation> enemySpawns = new List<EnemySpawnLocation>()
    {
        new EnemySpawnLocation(-3.08f, 0.55f, 0f),
        new EnemySpawnLocation(-1.91f, 0.55f, -3.164f),
        new EnemySpawnLocation(0.73f, 0.55f, -4.38f),
        new EnemySpawnLocation(2.87f, 0.55f, -3.65f),
        new EnemySpawnLocation(3.9f, 0.55f, -2.41f),
        new EnemySpawnLocation(4.35f, 0.55f, 0.55f),
        new EnemySpawnLocation(-1.88f, 0.55f, 4.03f),
        new EnemySpawnLocation(-4.64f, 0.55f, 2.8f),
        new EnemySpawnLocation(0.89f, 0.55f, -1.15f),
        new EnemySpawnLocation(1.78f, 0.55f, -2.64f)
    };

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());   
    }

    IEnumerator SpawnEnemy()
    {
        while(enemyCount < spawnLimit)
        {
            EnemySpawnLocation spawnPosition = enemySpawns[random.Next(enemySpawns.Count)];
            Debug.Log(spawnPosition.X + " " + spawnPosition.Y + " " + spawnPosition.Z);
            GameObject newEnemy = Instantiate(enemy);
            newEnemy.transform.SetParent(Environment.transform, false);
            newEnemy.transform.position = new Vector3(spawnPosition.X * 10, spawnPosition.Y, spawnPosition.Z * 10); // TODO: Need to figure out why position coords are displayed as *10^-1
            newEnemy.transform.rotation = Quaternion.identity;
            enemy.SetActive(true);
            enemyCount += 1;
            Debug.Log("enemy count" + enemyCount);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
