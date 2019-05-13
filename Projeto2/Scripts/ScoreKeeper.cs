using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ScoreKeeper : MonoBehaviour {

    //the score.
    public int[] score;
    //the game's UI element 
    public Text text; 

	// Use this for initialization
	void Start () {
        score = new int[2];
        //display the score to the screen
        UpdateScoreText();

	}

    void UpdateScoreText()
    {
        string toWrite = "Blue: " + score[0] + " \t" + "Red: " + score[1];
        text.text = toWrite;
    }

    //public method to be called from the Goal script
    public void ScoreGoal(int whichgoal)
    {
        //add one to the score
        score[whichgoal]++;
        //display the updated score
        UpdateScoreText();
    }

}
