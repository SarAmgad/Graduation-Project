using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignSpawner : MonoBehaviour
{
    // public GameObject objectToSpawn1;  // Assign in inspector
    // public GameObject objectToSpawn2;  // Assign in inspector
    // public Transform roadParent;      // Parent transform containing all road planes
    // public float minDistanceBetweenObjects = 1.0f; // Minimum distance between the two spawned objects
    // public float timeBetweenSpawns = 2.0f; // Time in seconds between the spawns
    // public float destroyAfterSeconds = 5.0f; // Time after which the objects should be destroyed

    // void Start()
    // {
    //     StartCoroutine(SpawnObjects());
    // }

    // IEnumerator SpawnObjects()
    // {
    //     // Spawn first object
    //     Vector3 spawnPosition1 = GetRandomPointOnRoad();
    //     if (spawnPosition1 != Vector3.zero)
    //     {
    //         GameObject obj1 = Instantiate(objectToSpawn1, spawnPosition1, Quaternion.identity);
    //         Destroy(obj1, destroyAfterSeconds);
    //     }

    //     // Wait for the specified time before spawning the second object
    //     yield return new WaitForSeconds(timeBetweenSpawns);

    //     // Spawn second object
    //     Vector3 spawnPosition2 = Vector3.zero;
    //     int maxAttempts = 10;
    //     int attempt = 0;
    //     while (attempt < maxAttempts)
    //     {
    //         spawnPosition2 = GetRandomPointOnRoad();
    //         if (Vector3.Distance(spawnPosition1, spawnPosition2) >= minDistanceBetweenObjects)
    //         {
    //             break;
    //         }
    //         attempt++;
    //     }

    //     if (spawnPosition2 != Vector3.zero)
    //     {
    //         GameObject obj2 = Instantiate(objectToSpawn2, spawnPosition2, Quaternion.identity);
    //         Destroy(obj2, destroyAfterSeconds);
    //     }
    // }

    // Vector3 GetRandomPointOnRoad()
    // {
    //     Vector3 randomPoint = Vector3.zero;
    //     int maxAttempts = 10;
    //     int attempt = 0;

    //     while (attempt < maxAttempts)
    //     {
    //         // Get a random plane from the road parent
    //         int randomIndex = Random.Range(0, roadParent.childCount);
    //         Transform randomPlane = roadParent.GetChild(randomIndex);

    //         // Get the bounds of the selected plane
    //         Bounds bounds = randomPlane.GetComponent<Renderer>().bounds;

    //         // Generate a random point within the bounds
    //         float randomX = Random.Range(bounds.min.x, bounds.max.x);
    //         float randomZ = Random.Range(bounds.min.z, bounds.max.z);
    //         Vector3 randomPosition = new Vector3(randomX, 0f, randomZ);

    //         // Use raycasting to ensure the point is on the plane
    //         RaycastHit hit;
    //         if (Physics.Raycast(randomPosition + Vector3.up * 10, Vector3.down, out hit))
    //         {
    //             if (hit.transform == randomPlane)
    //             {
    //                 randomPoint = hit.point;
    //                 randomPoint.y = 0f;
    //                 break;
    //             }
    //         }

    //         attempt++;
    //     }

    //     return randomPoint;
    // }
}
