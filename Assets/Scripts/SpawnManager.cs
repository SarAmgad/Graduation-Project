using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] prefabsNumbers;
    private readonly float z = 4.9266f;
    private int currentIndex = 0;
    private List<Vector3> usedPositions = new List<Vector3>();
    // private string testType;

    private Numbers numbers;
    List<int> visibleNumbers;
    
    

    void Start()
    {
        numbers = FindObjectOfType<Numbers>();
        if (numbers == null)
        {
            Debug.LogError("Numbers component not found in the scene!");
        }
        else {
            visibleNumbers = numbers.visibleNumbers;
            GeneratePositions();
        }
        Debug.Log(Vector3.Distance(new Vector3(-0.419f,1.595f,4.9266f), new Vector3(-0.508f,1.56602f,4.2966f)) + "-------------");
    }
    // void Start()
    // {
    //     // testType = PlayerPrefs.GetString("TestType");
    //     // if (currentIndex == 0)  // Ensure that instantiation only happens once
    //     // {
    //     //     StartCoroutine(InstantiatePrefabs());
    //     // }
        
    // }

    // IEnumerator InstantiatePrefabs()
    // {
    //     while (currentIndex < prefabsNumbers.Length)
    //     {
    //         Vector3 spawnPos = GenerateRandomPosition();

    //         Instantiate(prefabsNumbers[currentIndex], spawnPos, prefabsNumbers[currentIndex].transform.rotation);

    //         usedPositions.Add(spawnPos);
    //         currentIndex++;
    //         yield return null;
    //     }

    //     this.enabled = false;
    // }

    void GeneratePositions()
    {
        for(int i = 0; i < visibleNumbers.Count; i++)
        {
            InstantiatePrefab(i, true);
        }

    }

    public void InstantiatePrefab(int i, bool visibleFlag)
    {
        // float randomX, randomY;
        Vector3 spawnPos;
        // randomX = Random.Range(-1.695f, 2.301f);
        // randomY = Random.Range(1.03f, 2.306f);
        spawnPos = GenerateRandomPosition();

        // spawnPos = new Vector3(randomX, randomY, z);
        if (visibleFlag)
            Instantiate(prefabsNumbers[visibleNumbers[i]-1], spawnPos, prefabsNumbers[i].transform.rotation);
        else
            Instantiate(prefabsNumbers[i - 1], spawnPos, prefabsNumbers[i].transform.rotation);
        usedPositions.Add(spawnPos);
    }

    Vector3 GenerateRandomPosition()
    {
        float randomX, randomY;
        Vector3 spawnPos;
        int maxAttempts = 500; // Maximum number of attempts to find an unused position
        int attempts = 0; // Number of attempts made so far

        do
        {
            randomX = Random.Range(-1.695f, 2.301f);
            randomY = Random.Range(1.03f, 2.306f);
            spawnPos = new Vector3(randomX, randomY, z);
            attempts++;

            // Check if position is used
            if (!IsPositionUsed(spawnPos))
            {
                return spawnPos; // Return the position if it's not used
            }
        } while (attempts < maxAttempts);

        Debug.LogError("Failed to find an unused position after " + maxAttempts + " attempts.");
        return Vector3.zero; // Return zero vector if no unused position is found
    }


    bool IsPositionUsed(Vector3 position)
    {
        foreach (Vector3 usedPos in usedPositions)
        {
            if (Vector3.Distance(position, usedPos) < 1.1f)
            {
                return true;
            }
        }
        return false;
    }
}