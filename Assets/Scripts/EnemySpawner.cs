using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    [SerializeField] private BoxCollider[] spawnAreas;
    public static EnemySpawner Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        spawnAreas = GetComponentsInChildren<BoxCollider>();
    }


    public IEnumerator SpawnEnemy(GameObject spawnObject)
    {
        while (PlayerController.Instance.onRope)
        {
            yield return new WaitForSeconds(Random.Range(1f, 5f));
            // Choose a random spawn area
            BoxCollider spawnArea = spawnAreas[Random.Range(0, spawnAreas.Length)];
            //Debug.Log("current spawnarea" + spawnArea.name);

            // Generate a random position within the chosen spawn area
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
            );

            // Instantiate the enemy at the random position
            Instantiate(spawnObject, randomPosition, Quaternion.identity);
            Debug.Log("enemy spawned");
            
        }
    }

    public void StartSpawning(GameObject spawnObject)
    {
        StartCoroutine(SpawnEnemy(spawnObject));
    }
    
}
