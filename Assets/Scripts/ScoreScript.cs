using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_Text scoreSigns;
    [SerializeField] private TMP_Text scoreVoices;

    private SlowDownSign signs;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scoreSigns.text = "Correct Signs: " + StopSign.scoreForDetectedSigns.ToString();
        scoreVoices.text = "Correct Voices: " + Movement.score.ToString();
    }
}
