using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour {

    public int score;                   
    public int target_score;            
    public int arrow_number;            
    void Start()
    {
        score = 0;
        target_score = 15;
        arrow_number = 10;
    }
    //counting score
    public void Record(GameObject disk)
    {
        int temp = disk.GetComponent<RingData>().score;
        score = temp + score;
        //Debug.Log(score);
    }
}
