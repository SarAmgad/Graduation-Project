using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    Queue <GameObject> Roads = new Queue<GameObject>();
    Queue <Vector3> RoadPositions = new Queue<Vector3>();
    public GameObject firstRoad;
    // Queue 
    private float offset = 280f;


    public enum Direction
    {
        Forward,
        Left,
        Right
    }

    void Start()
    {
        Roads.Enqueue(firstRoad);
        RoadPositions.Enqueue(firstRoad.transform.position);
    }
    public void SpawnRoad(Direction direction, GameObject parent)
    {
        if(Roads.Count >= 6){
            Destroy(Roads.Dequeue());
            Destroy(Roads.Dequeue());
            // Roads.Dequeue();
            // Roads.Dequeue();
        }
        
        // if RoadPositions.Contains(parent.position))
        switch (direction)
        {
            case Direction.Forward:
            
                Transform forward = parent.transform.Find("forward empty");
                //debug.log("Forward direction " + parent.transform.position);
                if (forward != null && !RoadPositions.Contains(forward.position))
                    {
                    // Calculate the new position based on the child's position and the desired offset
                    Vector3 newPosition = forward.position;

                    // Instantiate a new road at the new position and with the same rotation as the child
                    GameObject newRoad = Instantiate(parent, newPosition, forward.rotation);

                    // Optionally add the newly instantiated road to the list if needed
                    // Roads.Add(newRoad);
                    Roads.Enqueue(newRoad);
                    RoadPositions.Enqueue(newPosition);

                    //debug.log("New road instantiated at position: " + newRoad.transform.position);
                    //debug.log("New road instantiated at position Forward: " + newRoad.transform.localPosition);
                }
                else
                {
                    //debug.log("Child named '60 empty' not found.");
                }

                break;
            case Direction.Left:
            //debug.log("Left direction " + parent.transform.position);
                Transform left = parent.transform.Find("-60 empty");

                if (left != null && !RoadPositions.Contains(left.position))
                {
                    // Do something with the specific child
                    //debug.log("Found child: " + left.name);

                    // Calculate the new position based on the child's position and the desired offset
                    Vector3 newPosition = left.position;

                    // Instantiate a new road at the new position and with the same rotation as the child
                    GameObject newRoad = Instantiate(parent, newPosition, left.rotation);

                    // Optionally add the newly instantiated road to the list if needed
                    Roads.Enqueue(newRoad);
                    RoadPositions.Enqueue(newPosition);

                    //debug.log("New road instantiated at position: " + newRoad.transform.position);
                    //debug.log("New road instantiated at position Leftt: " + newRoad.transform.localPosition);
                }
                else
                {
                    //debug.log("Child named '60 empty' not found.");
                }
            
                break;
            case Direction.Right:
                //debug.log("Right direction " + parent.transform.position);
                Transform right = parent.transform.Find("60 empty");

                if (right != null && !RoadPositions.Contains(right.position))
                {
                    // Do something with the specific child
                    //debug.log("Found child: " + right.name);

                    // Calculate the new position based on the child's position and the desired offset
                    Vector3 newPosition = right.position;

                    // Instantiate a new road at the new position and with the same rotation as the child
                    GameObject newRoad = Instantiate(parent, newPosition, right.rotation);

                    // Optionally add the newly instantiated road to the list if needed
                    Roads.Enqueue(newRoad);
                    RoadPositions.Enqueue(newPosition);

                    //debug.log("New road instantiated at position Righ Global: " + newRoad.transform.position);
                    //debug.log("New road instantiated at position Right Local: " + newRoad.transform.localPosition);
                }
                else
                {
                    //debug.log("Child named '60 empty' not found.");
                }
                
                break;
            default:
                //debug.logWarning("Invalid direction.");
                break;
        }
    }

    bool IsPositionUsed(Vector3 position)
    {
        // float dist;
        foreach (Vector3 usedPos in RoadPositions)
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
