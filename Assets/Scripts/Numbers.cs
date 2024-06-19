// using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
// using UnityEngine.SceneManagement;

public class Numbers : MonoBehaviour
{
    private List<int> allNumbers = Enumerable.Range(1, 20).ToList();
    public List<int> visibleNumbers = new List<int>();
    List<int> hiddenNumbers = new List<int>();
    private int mistakesNum;
    private SpawnManager spawnManager;
    public GameObject startMenu; 
    public GameObject endCanvas; 
    [SerializeField] TextMeshProUGUI msgText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GetComponent<SpawnManager>();
        if (spawnManager == null)
        {
            Debug.LogError("SpawnManager component not found on the same GameObject as Numbers!");
        }
        else
        {
            startList();
            PrintShownLists();
            PrintHiddenLists();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    void PrintHiddenLists(){
        string hiddenNumbersStr = string.Join(", ", hiddenNumbers);
        Debug.Log("======Hidden Numbers: " + hiddenNumbersStr);
    }

    void PrintShownLists(){
        string visibleNumbersStr = string.Join(", ", visibleNumbers);
        Debug.Log("Visible Numbers: " + visibleNumbersStr);
    }

    public void CheckMissingNumber(int num)
    {
        msgText.text = "";
        if (hiddenNumbers.Contains(num)){
            if (num == hiddenNumbers[0]){
                hiddenNumbers.RemoveAt(0);
                Debug.Log($"{num} removed");
                PrintHiddenLists();
                spawnManager.InstantiatePrefab(num, false);
                if (hiddenNumbers.Count == 0){
                    endCanvas.SetActive(true);
                }
            }
            else{
                msgText.text = "The order was not correct";
                Debug.Log("The order was not correct");
                mistakesNum++;
            }
        }
        else{
            msgText.text = $"That was not correct. \nThe {num} is not missing";
            Debug.Log($"The {num} is not missing");
            mistakesNum++;
        }
        if (mistakesNum == 4)
        {
            mistakesNum = 0;
            msgText.text = "You made several mistakes. Please view the task description once more.";
            foreach (var gameObj in GameObject.FindGameObjectsWithTag("Numbers")){
                Destroy(gameObj);
            }
            startMenu.SetActive(true);
            Start();
            // spawnManager.GeneratePositions();
        }
        PrintHiddenLists();
    }



}
