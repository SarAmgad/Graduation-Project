using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeNumber : MonoBehaviour
{
    List<int> visibleNumbers;
    private Numbers numbers;
    public GameObject[] prefabsNumbers;
    private readonly float z = 4.9266f;
    int parsedNum;
    

    void Start()
    {
        numbers = FindObjectOfType<Numbers>();
        if (numbers == null)
        {
            Debug.LogError("Numbers component not found in the scene!");
        }
        else {
            visibleNumbers = numbers.visibleNumbers;

            string num = gameObject.name;
            if (int.TryParse(num, out parsedNum))
            {
                InstantiatePrefab(parsedNum, false);
            }
            else
            {
                Debug.LogError("Failed to parse num to int!");
            }
            
            if (visibleNumbers.Contains(parsedNum))
                GeneratePositions();
        }
        // Debug.Log(prefabsNumbers[19].name + "-------------");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GeneratePositions()
    {
        // int parsedNum;
        // if (int.TryParse(num, out parsedNum))
        // {
            InstantiatePrefab(parsedNum, false);
        // }
        // else
        // {
        //     Debug.LogError("Failed to parse num to int!");
        // }
    }


    public void InstantiatePrefab(int i, bool visibleFlag)
    {
        float randomX, randomY;
        Vector3 spawnPos;
        randomX = Random.Range(-1.695f, 2.301f);
        randomY = Random.Range(1.03f, 2.306f);

        spawnPos = new Vector3(randomX, randomY, z);
        if (visibleFlag)
            Instantiate(prefabsNumbers[visibleNumbers[i]-1], spawnPos, prefabsNumbers[i].transform.rotation);
        else
            Instantiate(prefabsNumbers[i - 1], spawnPos, prefabsNumbers[i].transform.rotation);

        // if 
    }
}
