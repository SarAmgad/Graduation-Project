using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> Roads;
    private float offset = 60f;
    




    public enum Direction
{
    Forward,
    Left,
    Right
}






public void SpawnRoad(Direction direction, Vector3 position, Quaternion rotation)
{
    GameObject newRoad = Instantiate(Roads[0]); // Instantiate a new road
    Roads.RemoveAt(0); // Remove the road from the list

    float newZ = 0f;
    float newX = 0f;

    // Determine the new position based on the direction
    switch (direction)
    {
        case Direction.Forward:
            //newZ = Roads[Roads.Count - 1].transform.position.z + offset;



            if (rotation == Quaternion.Euler(0, 90, 0))
            {
               newZ = position.z ;
               newX = position.x + offset;
            }
            else if (rotation == Quaternion.Euler(0, -90, 0))
            {
                newZ = position.z ;
                newX = position.x - offset;
            }
            else if (rotation == Quaternion.Euler(0, -180, 0))
            {
                newZ = position.z - offset;
                newX = position.x ;
            }
            else if (rotation == Quaternion.Euler(0, 180, 0))
            {
                newZ = position.z - offset;
                newX = position.x ;
            }
            else
            {
               newZ = position.z + offset;
               newX = position.x;
            }
            
            break;
        case Direction.Left:
            if (rotation == Quaternion.Euler(0, 90, 0))
            {
               newZ = position.z ;
               newX = position.x + offset;
            }
            else if (rotation == Quaternion.Euler(0, -90, 0))
            {
                newZ = position.z ;
                newX = position.x - offset;
            }
            else if (rotation == Quaternion.Euler(0, -180, 0))
            {
                newZ = position.z - offset;
                newX = position.x ;
            }
            else if (rotation == Quaternion.Euler(0, 180, 0))
            {
                newZ = position.z - offset;
                newX = position.x ;
            }
            else
            {
               newZ = position.z + offset;
               newX = position.x;
            }
            break;
        case Direction.Right:
            if (rotation == Quaternion.Euler(0, 90, 0))
            {
               newZ = position.z ;
               newX = position.x + offset;
            }
            else if (rotation == Quaternion.Euler(0, -90, 0))
            {
                newZ = position.z ;
                newX = position.x - offset;
            }
            else if (rotation == Quaternion.Euler(0, -180, 0))
            {
                newZ = position.z - offset;
                newX = position.x ;
            }
            else if (rotation == Quaternion.Euler(0, 180, 0))
            {
                newZ = position.z - offset;
                newX = position.x ;
            }
            else
            {
               newZ = position.z + offset;
               newX = position.x;
            }
            break;
        default:
            break;
    }

    // Set the new position and rotation of the spawned road
    newRoad.transform.position = new Vector3(newX, 0, newZ);
    newRoad.transform.rotation = rotation;

    Roads.Add(newRoad); // Add the new road to the list
}



/*public void SpawnRoad(Direction direction , Vector3 position,Quaternion rotation)
{
    GameObject newRoad = Instantiate(Roads[0]); // Instantiate a new road
    Roads.Remove(newRoad); // Remove the road from the list

    float newZ1 = Roads[Roads.Count - 1].transform.position.z + offset;
    float newZ2 = Roads[Roads.Count - 1].transform.position.z - offset;
    float newx1 = Roads[Roads.Count - 1].transform.position.x + offset;
    float newx2 = Roads[Roads.Count - 1].transform.position.x - offset;
    
    //Quaternion rotation = Quaternion.identity;
    if(rotation == Quaternion.Euler(0, 90, 0))
        {
                
                
            newRoad.transform.position = new Vector3(newx1 , 0, position.z  );
            newRoad.transform.rotation =Quaternion.Euler(0, 90, 0);
                
        }
    if(rotation == Quaternion.Euler(0, -90, 0))
        {
                
                
            newRoad.transform.position = new Vector3(newx2 , 0, position.z  );
            newRoad.transform.rotation =rotation;
                
        }
    if(rotation == Quaternion.Euler(0, 180, 0))
        {
                
                
            newRoad.transform.position = new Vector3(position.x , 0, newZ2);
            newRoad.transform.rotation =rotation;
                
        }
    if(rotation == Quaternion.Euler(0, -180, 0))
        {
                
                
            newRoad.transform.position = new Vector3(position.x  , 0, newZ2);
            newRoad.transform.rotation =rotation;
                
        }
     else
        {
            newRoad.transform.position = new Vector3(position.x , 0, newZ1);
            newRoad.transform.rotation =rotation;

         } 

    /*switch (direction)
    {
        case Direction.Forward:

           if(rotation == Quaternion.Euler(0, 90, 0))
            {
                
                
                newRoad.transform.position = new Vector3(newX + offset , 0, position.z  );
                newRoad.transform.rotation =rotation;
                
            }
            if(rotation == Quaternion.Euler(0, -90, 0))
            {
                
                
                newRoad.transform.position = new Vector3(newX -offset , 0, position.z  );
                newRoad.transform.rotation =rotation;
                
            }
            else
            {
                newRoad.transform.position = new Vector3(0 , 0, newZ);
                newRoad.transform.rotation =rotation;

            } 
            break;
        case Direction.Left:
            newRoad.transform.position = new Vector3(newX - offset, 0, position.z);
            newRoad.transform.rotation = rotation;
            break;
        case Direction.Right:
            newRoad.transform.position = new Vector3(newX+offset , 0, position.z);
            newRoad.transform.rotation = rotation;
            //newRoad.transform.rotation = Quaternion.Euler(0, 90, 0);
            break;
        default:
            break;
    }*/

   /* Roads.Add(newRoad); // Add the new road to the list
 }*/


   /* public void MoveRoad()
    {
        GameObject movedroad = Roads[0];
        Roads.Remove(movedroad);
        float newz = Roads[Roads.Count-1].transform.position.z + offset;
        movedroad.transform.position = new Vector3 (0,0,newz);
        Roads.Add(movedroad);

    }

    public void MoveRoadRight(Vector3 position)
    {
        GameObject movedroad = Roads[0];
        Roads.Remove(movedroad);
        /*float newz = Roads[Roads.Count-1].transform.position.z + offset;
        float newx = Roads[Roads.Count-1].transform.position.x + 30;*/
       /* movedroad.transform.Rotate(Vector3.up, 90f);
        movedroad.transform.position = position;
        Roads.Add(movedroad);

    }*/
}
