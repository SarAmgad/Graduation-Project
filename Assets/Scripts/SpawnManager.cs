// using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] prefabsNumbers;
    private readonly float z = 4.9469f;
    private List<Vector3> usedPositions = new List<Vector3>();
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
        }
    }

    public void GeneratePositions()
    {
        visibleNumbers = numbers.visibleNumbers;
        usedPositions.Clear();
        for(int i = 0; i < visibleNumbers.Count; i++)
        {
            InstantiatePrefab(i, true);
        }
    }

    public void InstantiatePrefab(int i, bool visibleFlag)
    {
        Vector3 spawnPos;
        spawnPos = GenerateRandomPosition();
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
        int maxAttempts = 1000; 
        int attempts = 0;

        do
        {
            randomX = Random.Range(-1.65f, 2.35f);
            randomY = Random.Range(1, 2.3f);
            spawnPos = new Vector3(randomX, randomY, z);
            attempts++;

            // Check if position is used
            if (!IsPositionUsed(spawnPos))
            {
                return spawnPos; 
            }
        } while (attempts < maxAttempts);

        Debug.LogError("Failed to find an unused position after " + maxAttempts + " attempts.");
        return spawnPos; //If failed to find a postion, assign last position
    }


    bool IsPositionUsed(Vector3 position)
    {
        // float dist;
        foreach (Vector3 usedPos in usedPositions)
        {
            // dist = ;
            if (Vector3.Distance(position, usedPos) < 0.65f)
            {
                return true;
            }
        }
        return false;
    }
}