using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public List<GameObject> spawnedEnemies;
    public List<GameObject> killedEnemies;
    public int numberToSpawnEnemies;

    public Transform movePointRight;
    public Transform movePointLeft;


    int spawnedCount;
    int killedCount;

    private void Start()
    {
        SpawnMobs();
        StartCoroutine(SpawnerCoroutine());
    }

    IEnumerator SpawnerCoroutine()
    {
        while (true)
        {

            SpawnMobs();

            yield return new WaitForSeconds(10);
        }
    }

    void SpawnMobs()
    {        
        for(int i=0; i<numberToSpawnEnemies; i++)
        {
            int randomMobIndex = Random.Range(0, enemyPrefabs.Length);
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
            Transform randomSpawnPoint = spawnPoints[randomSpawnPointIndex];
            GameObject spawnMob = Instantiate(enemyPrefabs[randomMobIndex], randomSpawnPoint.position, Quaternion.identity);
            spawnedEnemies.Add(spawnMob);
        }       
    }
}
