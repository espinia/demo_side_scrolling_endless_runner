using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    private float spawnDelay = 2;
    private float spawnInterval = 1.5f;

    private PlayerControllerX playerControllerScript;

    private Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position;
        InvokeRepeating("SpawnObjects", spawnDelay, spawnInterval);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    // Spawn obstacles
    void SpawnObjects ()
    {
        // If game is still active, spawn new object
        if (!playerControllerScript.GameOver)
        {
            // Set random spawn location and random object index
            Vector3 spawnLocation = new Vector3(spawnPos.x, Random.Range(5, 15), spawnPos.z);
            int index = Random.Range(0, objectPrefabs.Length);

            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }

    }
}
