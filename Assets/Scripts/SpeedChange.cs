using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChange : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> gameObjects;
    public Material specialMaterial;
    public float durationForLight = 3f;
    public float durationForNoLight = 6f;
    public float initialDelay = 3f; // Initial delay in seconds

   // private bool isColliding = false;

    private List<Material> originalMaterials = new List<Material>();
    private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

    void Start()
    {
        // Store the original materials and disable mesh renderers
        foreach (var obj in gameObjects)
        {
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                originalMaterials.Add(renderer.material);
                meshRenderers.Add(renderer);
                // renderer.enabled = false; 
            }
        }

        StartCoroutine(StartAfterDelay(initialDelay));
    }

    IEnumerator StartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(AssignRandomMaterial());
    }

    IEnumerator AssignRandomMaterial()
    {
        while (true)
        {

            int randomIndex = Random.Range(0, gameObjects.Count);
            MeshRenderer selectedRenderer = meshRenderers[randomIndex];
            selectedRenderer.enabled = true;
            selectedRenderer.material = specialMaterial;

            yield return new WaitForSeconds(durationForLight);


            selectedRenderer.material = originalMaterials[randomIndex];
            selectedRenderer.enabled = false;


            yield return new WaitForSeconds(durationForNoLight);

            Debug.Log("Collisionnnnnn.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
