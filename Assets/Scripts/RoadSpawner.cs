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

    public void SpawnRoad(Direction direction, GameObject parent, Vector3 position, Quaternion rotation)
    {
        switch (direction)
        {
            case Direction.Forward:
            
                Transform forward = parent.transform.Find("forward empty");
                Debug.Log("Forward direction " + parent.transform.position);
                if (forward != null)
                    {
                    // Calculate the new position based on the child's position and the desired offset
                    Vector3 newPosition = forward.position;

                    // Instantiate a new road at the new position and with the same rotation as the child
                    GameObject newRoad = Instantiate(parent, newPosition, forward.rotation);

                    // Optionally add the newly instantiated road to the list if needed
                    Roads.Add(newRoad);

                    Debug.Log("New road instantiated at position: " + newRoad.transform.position);
                    Debug.Log("New road instantiated at position Forward: " + newRoad.transform.localPosition);
                }
                else
                {
                    Debug.Log("Child named '60 empty' not found.");
                }

                break;
            case Direction.Left:
            Debug.Log("Left direction " + parent.transform.position);
                Transform specificChild2 = parent.transform.Find("-60 empty");

                if (specificChild2 != null)
                {
                    // Do something with the specific child
                    Debug.Log("Found child: " + specificChild2.name);

                    // Calculate the new position based on the child's position and the desired offset
                    Vector3 newPosition = specificChild2.position;

                    // Instantiate a new road at the new position and with the same rotation as the child
                    GameObject newRoad = Instantiate(parent, newPosition, specificChild2.rotation);

                    // Optionally add the newly instantiated road to the list if needed
                    Roads.Add(newRoad);

                    Debug.Log("New road instantiated at position: " + newRoad.transform.position);
                    Debug.Log("New road instantiated at position Leftt: " + newRoad.transform.localPosition);
                }
                else
                {
                    Debug.Log("Child named '60 empty' not found.");
                }
            
                break;
            case Direction.Right:
                Debug.Log("Right direction " + parent.transform.position);
                Transform specificChild = parent.transform.Find("60 empty");

                if (specificChild != null)
                {
                    // Do something with the specific child
                    Debug.Log("Found child: " + specificChild.name);

                    // Calculate the new position based on the child's position and the desired offset
                    Vector3 newPosition = specificChild.position;

                    // Instantiate a new road at the new position and with the same rotation as the child
                    GameObject newRoad = Instantiate(parent, newPosition, specificChild.rotation);

                    // Optionally add the newly instantiated road to the list if needed
                    Roads.Add(newRoad);

                    Debug.Log("New road instantiated at position Righ Global: " + newRoad.transform.position);
                    Debug.Log("New road instantiated at position Right Local: " + newRoad.transform.localPosition);
                }
                else
                {
                    Debug.Log("Child named '60 empty' not found.");
                }
                
                break;
            default:
                Debug.LogWarning("Invalid direction.");
                break;
        }
    }

}
