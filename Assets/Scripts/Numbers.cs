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
    private NumberSpawnManager numberSpawnManager;
    public GameObject startMenu, endCanvas, mistakeCanvas, resultUi; 
    [SerializeField] TextMeshProUGUI msgText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        numberSpawnManager = GetComponent<NumberSpawnManager>();
        if (numberSpawnManager == null)
        {
            Debug.LogError("SpawnManager component not found on the same GameObject as Numbers!");
        }
        else
        {
            startList();
            PrintHiddenLists();
        }
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
        Debug.Log("Hidden Numbers: " + hiddenNumbersStr);
    }

    public void CheckMissingNumber(int num)
    {
        msgText.text = "";
        resultUi.SetActive(false);
        if (hiddenNumbers.Contains(num)){
            if (num == hiddenNumbers[0]){
                hiddenNumbers.RemoveAt(0);
                numberSpawnManager.InstantiatePrefab(num, false);
                if (hiddenNumbers.Count == 0){
                    endCanvas.SetActive(true);
                }
            }
            else{
                resultUi.SetActive(true);
                msgText.text = "The order was not correct";
                mistakesNum++;
            }
        }
        else{
            resultUi.SetActive(true);
            msgText.text = $"That was not correct. \nThe {num} is not missing";
            mistakesNum++;
        }
        if (mistakesNum == 4)
        {
            mistakesNum = 0;
            mistakeCanvas.SetActive(true);
        }
        PrintHiddenLists();
    }

    public void Replay(){
        foreach (var gameObj in GameObject.FindGameObjectsWithTag("Numbers")){
            Destroy(gameObj);
        }
        startMenu.SetActive(true);
        Start();
    }



}
