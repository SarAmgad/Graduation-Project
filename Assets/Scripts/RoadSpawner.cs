using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> Roads;
    private float offset = 280f;


    public enum Direction
    {
        Forward,
        Left,
        Right
    }







    public void SpawnRoad(Direction direction, Vector3 position, Quaternion rotation)
    {
        if (Roads == null || Roads.Count == 0)
        {
            Debug.LogWarning("No roads available to spawn.");
            return;
        }

        // Instantiate a new road
        GameObject newRoad = Instantiate(Roads[0]);
        GameObject oldRoad = Roads[0];

        // Remove the old road from the list
        if (!(Roads.Count == 1))
        {
            Roads.RemoveAt(0);
            Destroy(oldRoad, 120);
        }



        float newZ = 0f;
        float newX = 0f;

        // Determine the new position based on the direction
        switch (direction)
        {
            case Direction.Forward:
                if (rotation == Quaternion.Euler(0, 180, 0))
                {
                    newX = position.x + offset;
                    newZ = position.z;
                }
                else if (rotation == Quaternion.Euler(0, 60, 0) || rotation == Quaternion.Euler(0, 240, 0))
                {
                    newX = position.x + 236;
                    newZ = position.z + 132.1f;
                    Debug.Log("right forward");
                }
                else if (rotation == Quaternion.Euler(0, -60, 0) || rotation == Quaternion.Euler(0, -240, 0))
                {
                    newX = position.x - 456;
                    newZ = position.z + 260;
                }
                else if (rotation == Quaternion.Euler(0, -120, 0))
                {
                    newX = position.x - 240;
                    newZ = position.z - 136;
                    Debug.Log("-120 forward  " + position.x + "  " + position.z);
                }
                else if (rotation == Quaternion.Euler(0, 120, 0))
                {
                    newX = position.x + 83;
                    newZ = position.z - 221;
                    Debug.Log("right forward");
                }
                else
                {
                    newX = position.x;
                    newZ = position.z + offset;
                }
                break;
            case Direction.Left:
                if (rotation == Quaternion.Euler(0, -60, 0))
                {
                    newX = position.x - 95;
                    newZ = position.z + 55;
                }
                else if (rotation == Quaternion.Euler(0, -180, 0))
                {
                    newX = position.x;
                    newZ = position.z - offset;
                }
                else if (rotation == Quaternion.Euler(0, -120, 0))
                {
                    newX = position.x - 97.7f;
                    newZ = position.z - 55;
                    Debug.Log("Right -120  " + position.x + "  " + position.z);
                }
                else
                {
                    newX = position.x - offset;
                    newZ = position.z;
                }
                break;
            case Direction.Right:
                if (rotation == Quaternion.Euler(0, 60, 0))
                {
                    newX = position.x + 196;
                    newZ = position.z + 110;
                    Debug.Log("Right 60  " + position.x + "  " + position.z);
                }
                else if (rotation == Quaternion.Euler(0, -60, 0))
                {
                    newX = position.x;
                    newZ = position.z + offset;
                }
                else if (rotation == Quaternion.Euler(0, 120, 0))
                {
                    newX = position.x + 193;
                    newZ = position.z - 109;
                    Debug.Log("Right 120  " + position.x + "  " + position.z);
                }
                else
                {
                    newX = position.x;
                    newZ = position.z + 196.3f;
                }
                break;
            default:
                Debug.LogWarning("Invalid direction.");
                break;
        }

        // Set the new position and rotation of the spawned road
        newRoad.transform.position = new Vector3(newX, 0, newZ);
        newRoad.transform.rotation = rotation;

        // Add the newly instantiated road to the list
        Roads.Add(newRoad);

        // Debug logging
        Debug.Log("New road spawned at position: " + newRoad.transform.position);
        Debug.Log("Current number of roads in the list: " + Roads.Count);
    }






}
