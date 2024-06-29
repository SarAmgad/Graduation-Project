using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCounter : MonoBehaviour
{
    float time;
    public GameObject timerUI;
    // Start is called before the first frame update
    void Start()
    {
        time = 120f;
    }

    // Update is called once per frame
    void Update()
    {
        if(time <= 0){
            timerUI.SetActive(true); 
            StartCoroutine(Delay(2f)); 
            Debug.Log("time finished");          
        }
        else {
            time -= Time.deltaTime;
            Debug.Log("Time " + time);
        }
        
    }

    IEnumerator Delay(float delay){
        yield return new WaitForSeconds(delay);
        // Backtostart
        // StopSign.scoreForDetectedSigns
        // movement.score
    }
}
