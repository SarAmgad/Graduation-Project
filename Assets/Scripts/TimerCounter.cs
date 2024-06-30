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
        time = 200f;
    }

    // Update is called once per frame
    void Update()
    {
        if(time <= 0){
            timerUI.SetActive(true); 
            StartCoroutine(Delay(2f)); 
           //Debug.log("time finished");          
        }
        else {
            time -= Time.deltaTime;
           //Debug.log("Time " + time);
        }
        
    }

    IEnumerator Delay(float delay){
        yield return new WaitForSeconds(delay);
        StartingScene.BackToStart();
        
        // Backtostart
        // StopSign.scoreForDetectedSigns
        // movement.score
    }
}
