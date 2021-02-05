using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    
    [SerializeField]
    private GameObject enemyPrefab;

    private Vector3 _terrainDimensions;
    
    private void Awake()
    {
        _terrainDimensions = GetComponent<Terrain>().terrainData.size / 2;
    }

    void SpawnEnemy()
    {
        var minX = -_terrainDimensions.x;
        var maxX = _terrainDimensions.x;
        
        var minZ = -_terrainDimensions.z;
        var maxZ = _terrainDimensions.z;
        
        var spawnPos = new Vector3(Random.Range(minX, maxX), 2, Random.Range(minZ, maxZ));
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity).GetComponent<enemyController>().target = player;
    }

    private void Update()
    {
        if (Input.GetKeyUp("w"))
        {
            SpawnEnemy();
        }
    }
}
