using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int size;
    public float interval;
    public GameObject enemyPrefab;

    private float nextSpawn = 0f;

    internal void Update()
    {
        if (Time.timeSinceLevelLoad > nextSpawn)
        {
            int lastZone = -1;
            for (int i = 0; i < size; i++)
            {
                int zone = Random.Range(0, 6), degree = Random.Range(1, 4);
                while (zone == lastZone)
                {
                    zone = Random.Range(0, 6);
                    degree = Random.Range(1, 4);
                }
                Quaternion spawnRot = Quaternion.AngleAxis(60 * zone + 15 * degree, Vector3.up);
                Vector3 spawnDist = new(0f, 2f, 45f);
                GameObject enemy = Instantiate(enemyPrefab, spawnRot * spawnDist, Quaternion.identity, transform);
                EnemyController enemyController = enemy.GetComponent<EnemyController>();
                enemyController.region = (Card.Region)(zone / 2);
                enemyController.SetHp(Random.Range(1, 4));
                lastZone = zone;
            }
            nextSpawn += interval;
        }
    }
}
