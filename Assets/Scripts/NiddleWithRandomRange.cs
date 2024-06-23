using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiddleWithRandomRange : MonoBehaviour
{
    public Material specialMaterial;
    private Coroutine checkCollisionCoroutine;
    private bool isColliding = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Renderer>().material == specialMaterial)
        {
            isColliding = true;
            checkCollisionCoroutine ??= StartCoroutine(CheckCollisionDuration());
            Debug.Log("CollisionnnnnnEntered.");
        }
        Debug.Log("CollisionnnnnnEntered.");
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetComponent<Renderer>().material == specialMaterial)
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
            Debug.Log("Collisionnnnnn.");
        }
    }

}
