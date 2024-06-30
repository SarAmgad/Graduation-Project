using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiddleWithRandomRange : MonoBehaviour
{
    public Material specialMaterial;
    private Coroutine checkCollisionCoroutine;
    private bool isColliding = false;

    public static int score = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Renderer>().material == specialMaterial)
        {
            isColliding = true;
            checkCollisionCoroutine ??= StartCoroutine(CheckCollisionDuration());
           //Debug.log("Needleee CollisionnnnnnEntered. 1");
        }
       //Debug.log("Needleee CollisionnnnnnEntered. 2");

    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Renderer>().material == specialMaterial)
        {
            isColliding = false;
            if (checkCollisionCoroutine != null)
            {
                StopCoroutine(checkCollisionCoroutine);
                checkCollisionCoroutine = null;
            }
        }
    }

    IEnumerator CheckCollisionDuration()
    {
        yield return new WaitForSeconds(3f); // Change the duration as needed

        if (isColliding)
        {
            score++;
           //Debug.log(" Needleee Collisionnnnnn. 3" + score);
        }
    }

}
