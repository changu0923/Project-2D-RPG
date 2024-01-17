using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;

    public Transform movePointRight;
    public Transform movePointLeft;

    List<GameObject> spawnedEnemies = new List<GameObject>();
    bool isMobCountZero;
    [SerializeField] Coroutine activatedCoroutine;
    private void Start()
    {
        activatedCoroutine = StartCoroutine(SpawnerCoroutine());        
    }

    IEnumerator SpawnerCoroutine()
    {
        CheckMobsCount();
        while (isMobCountZero==true)
        {
            isMobCountZero = false;
            SpawnMobs();
            yield return new WaitForSeconds(10);
        }
    }

    void SpawnMobs()
    {
        int numberToSpawnEnemies = spawnPoints.Length;
        for(int i=0; i<numberToSpawnEnemies; i++)
        {
            int randomMobIndex = Random.Range(0, enemyPrefabs.Length);
            float randomX = Random.Range(-1.5f, 1.5f);
            Vector2 addX = new Vector2(randomX, 0f);
            Vector2 randomSpawnPoint = (Vector2)spawnPoints[i].position + addX;
            GameObject spawnMob = Instantiate(enemyPrefabs[randomMobIndex], randomSpawnPoint, Quaternion.identity);
            spawnedEnemies.Add(spawnMob);
        }       
    }

    void CheckMobsCount()
    {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (spawnedEnemies[i] == null)
            {            
                spawnedEnemies.RemoveAt(i);
            }
        }
        
        if (spawnedEnemies.Count == 0)
        {
            isMobCountZero = true;
        }
        else if(spawnedEnemies.Count != 0)
        {
            isMobCountZero = false;
        }

    }
}
