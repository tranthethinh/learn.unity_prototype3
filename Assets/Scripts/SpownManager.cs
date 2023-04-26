using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpownManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private Vector3 spawnPos;
    private float startDelay = 2.0f;
    //private float repeatRate = 2.0f;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        Invoke("SpawnObstacle", startDelay);

    }
    // Update is called once per frame
    void Update()
    {
       

    }
    void SpawnObstacle()
    {
        spawnPos = new Vector3(Random.Range(24,30), 0, 0);
        int i = Random.Range(0, obstaclePrefab.Length);
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab[i], spawnPos, obstaclePrefab[i].transform.rotation);
            Invoke("SpawnObstacle", Random.Range(2.0f, 4f));
        }
    }
}
