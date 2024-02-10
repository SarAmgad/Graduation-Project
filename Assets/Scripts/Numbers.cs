using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Numbers : MonoBehaviour
{
    private List<int> allNumbers = Enumerable.Range(1, 20).ToList();
    List<int> visibleNumbers = new List<int>();
    List<int> hiddenNumbers = new List<int>();
    
    void startList(){
        int cnt = 0;
        visibleNumbers.Clear();
        hiddenNumbers.Clear();
        foreach (int num in allNumbers)
        {
            int rnd = Random.Range(0, 2); 
            if ((rnd == 0) || (cnt > allNumbers.Count/3)) 
            {
                visibleNumbers.Add(num);
            }
            else
            {
                hiddenNumbers.Add(num);
                cnt ++;
            }
        }

    }
    
    // Start is called before the first frame update
    void Start()
    {
        // startList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() {
        startList();
        string visibleNumbersStr = string.Join(", ", visibleNumbers);
        string hiddenNumbersStr = string.Join(", ", hiddenNumbers);

        Debug.Log("Visible Numbers: " + visibleNumbersStr);
        Debug.Log("Hidden Numbers: " + hiddenNumbersStr);
    }



}
