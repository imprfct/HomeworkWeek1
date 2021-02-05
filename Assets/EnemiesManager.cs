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
    private Vector3 _terrainPosition;
    // Переменная помогает не спавнить врагов в горах :3
    private float Borders = 30f;
    
    private void Awake()
    {
        var terrain = GetComponent<Terrain>();
        
        _terrainDimensions = terrain.terrainData.size;
        _terrainPosition = terrain.transform.position;
    }

    void SpawnEnemy()
    {
        Debug.Log(_terrainPosition + " " + _terrainDimensions);
        var minX = _terrainPosition.x + Borders;
        var maxX = _terrainDimensions.x - Borders;
        
        var minZ = _terrainPosition.z + Borders;
        var maxZ = _terrainDimensions.z - Borders;
        
        var spawnPos = new Vector3(Random.Range(minX, maxX), 2, Random.Range(minZ, maxZ));
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity).GetComponent<enemyController>().target = player;
    }

    private float newEnemyCooldown = 200f;
    private void FixedUpdate()
    {
        newEnemyCooldown -= Time.fixedDeltaTime * 100;
        if (newEnemyCooldown <= 0)
        {
            SpawnEnemy();
            newEnemyCooldown = 200f;
        }
    }
}
