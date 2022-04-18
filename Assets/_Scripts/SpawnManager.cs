using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject [] obstaclePrefabs;

    private float startDelay = 2.0f;
    private float repeatRate = 2.0f;

    private Vector3 spawnPos;
    private Vector3 spawnPosBarrel;
    private PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position;
        spawnPosBarrel = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
	{
        if (!_playerController.GameOver)
        {
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            if (obstaclePrefab.name == "Barrel")
			{
                Instantiate(obstaclePrefab, spawnPosBarrel, obstaclePrefab.transform.rotation);
            }
            else
                Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }
}
