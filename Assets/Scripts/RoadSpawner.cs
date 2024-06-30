using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    Queue <GameObject> Roads = new Queue<GameObject>();
    Queue <Vector3> RoadPositions = new Queue<Vector3>();
    public GameObject firstRoad;
    List <string> childRandomize = new List<string>(){ "RightAudio", "LeftAudio", "RightAudio (1)", "sign_5 Variant", "sign_92 Variant"};
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
        }
        
        switch (direction)
        {
            case Direction.Forward:
                CreatObject(parent, "forward empty");

                
                

                break;
            case Direction.Left:
                CreatObject(parent, "-60 empty");
                // Transform left = parent.transform.Find("-60 empty");

                // if (left != null && !RoadPositions.Contains(left.position))
                // {
                //     Vector3 newPosition = left.position;
                //     GameObject newRoad = Instantiate(parent, newPosition, left.rotation);
                //     Roads.Enqueue(newRoad);
                //     RoadPositions.Enqueue(newPosition);
                // }
                
                break;
            case Direction.Right:
                CreatObject(parent, "60 empty");
                // Transform right = parent.transform.Find();

                // if (right != null && !RoadPositions.Contains(right.position))
                // {
                //     Vector3 newPosition = right.position;
                //     GameObject newRoad = Instantiate(parent, newPosition, right.rotation);

                //     Roads.Enqueue(newRoad);
                //     RoadPositions.Enqueue(newPosition);

                // }
                
                break;
            default:
                break;
        }
    }

    void CreatObject(GameObject parent, string childName){
        Transform direction = parent.transform.Find(childName);
        if (direction != null && !RoadPositions.Contains(direction.position))
        {
            Vector3 newPosition = direction.position;
            GameObject newRoad = Instantiate(parent, newPosition, direction.rotation);

            Roads.Enqueue(newRoad);
            RoadPositions.Enqueue(newPosition);

            foreach (string child in childRandomize){
                Transform audioTransform = newRoad.transform.Find(child);
                if (audioTransform != null)
                {
                    bool randomActivation = Random.value > 0.5f;
                    audioTransform.gameObject.SetActive(randomActivation);
                }
            }

        }
    }

}
