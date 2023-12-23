using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabRock0;
    [SerializeField]
    GameObject prefabRock1;
    [SerializeField]
    GameObject prefabRock2;

    const float MinSpawnDelay = 0;
    const float MaxSpawnDelay = 1;
    Timer spawnTimer;

    const int SpawnBorderSize = 100;
    int minSpawnX;
    int minSpawnY;
    int maxSpawnX;
    int maxSpawnY;

    int prefabCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        minSpawnX = SpawnBorderSize;
        maxSpawnX = Screen.width - SpawnBorderSize;
        minSpawnY = SpawnBorderSize;
        maxSpawnY = Screen.height - SpawnBorderSize;

        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = Random.Range(MinSpawnDelay, MaxSpawnDelay);
        spawnTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer.Finished && prefabCount < 8)
        {
            SpawnRock();

            // change spawn timer duration and restart
            spawnTimer.Duration = Random.Range(MinSpawnDelay, MaxSpawnDelay);
            spawnTimer.Run();
        }
    }

    void SpawnRock()
    {
        // generate random location and create new teddy bear
        Vector3 location = new Vector3(Random.Range(minSpawnX, maxSpawnX),
            Random.Range(minSpawnY, maxSpawnY),
            -Camera.main.transform.position.z);
        Vector3 worldLocation = Camera.main.ScreenToWorldPoint(location);

        GameObject rock;
        int spriteNumber = Random.Range(0, 3);
        if (spriteNumber == 0)
        {
            rock = Instantiate<GameObject>(prefabRock0,
                worldLocation, Quaternion.identity); ;
            prefabCount++;
        }
        else if (spriteNumber == 1)
        {
            rock = Instantiate<GameObject>(prefabRock1,
                worldLocation, Quaternion.identity); ;
            prefabCount++;
        }
        else
        {
            rock = Instantiate<GameObject>(prefabRock2,
                worldLocation, Quaternion.identity); ;
            prefabCount++;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        Debug.Log("Girdi.");//Temas eden prefableri siler
        prefabCount--;
    }
}
