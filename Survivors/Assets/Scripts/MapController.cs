using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    public GameObject latestChunk;
    public float maximumOptimizationDistance; //Must be greater than the length and width of the tilemap
    public float optimizationCooldownDuration; //if set to 1, will call the optimization every second

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;

    Vector3 noTerrainPosition;
    PlayerMovement playerMovement;
    float optimizationDistance;
    float optimizationCooldown;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if(!currentChunk) //fail safe
        {
            return;
        }

        if(playerMovement.moveDir.x > 0 && playerMovement.moveDir.y == 0) //right
        {
            //20 is the size of the tile map if you have done 20x20
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;
                SpawnChunk();
            }
        }
        else if (playerMovement.moveDir.x < 0 && playerMovement.moveDir.y == 0) //left
        {
            //20 is the size of the tile map if you have done 20x20
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;
                SpawnChunk();
            }
        }
        else if (playerMovement.moveDir.x == 0 && playerMovement.moveDir.y > 0) //up
        {
            //20 is the size of the tile map if you have done 20x20
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Up").position;
                SpawnChunk();
            }
        }
        else if (playerMovement.moveDir.x == 0 && playerMovement.moveDir.y < 0) //down
        {
            //20 is the size of the tile map if you have done 20x20
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down").position;
                SpawnChunk();
            }
        }
        else if (playerMovement.moveDir.x > 0 && playerMovement.moveDir.y > 0) //right up
        {
            //20 is the size of the tile map if you have done 20x20
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Up").position;
                SpawnChunk();
            }
        }
        else if (playerMovement.moveDir.x > 0 && playerMovement.moveDir.y < 0) //right down
        {
            //20 is the size of the tile map if you have done 20x20
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Down").position;
                SpawnChunk();
            }
        }
        else if (playerMovement.moveDir.x < 0 && playerMovement.moveDir.y > 0) //left up
        {
            //20 is the size of the tile map if you have done 20x20
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left Up").position;
                SpawnChunk();
            }
        }
        else if (playerMovement.moveDir.x < 0 && playerMovement.moveDir.y < 0) //left down
        {
            //20 is the size of the tile map if you have done 20x20
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left Down").position;
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizationCooldown -= Time.deltaTime;

        if(optimizationCooldown <= 0)
        {
            optimizationCooldown = optimizationCooldownDuration;
        }
        else
        {
            return;
        }

        foreach(GameObject chunk in spawnedChunks)
        {
            optimizationDistance = Vector3.Distance(player.transform.position, chunk.transform.position);

            if(optimizationDistance > maximumOptimizationDistance)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
