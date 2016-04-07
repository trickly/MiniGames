using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RPSFinalScore : MonoBehaviour {
    public Text finalScore;

    //Used for the endgame state to display the final results
    void Start()
    {

        finalScore = GameObject.Find("FinalScore").GetComponent<Text>();
        finalScore.text = "ME: " + GameObject.Find("Main CameraG").GetComponent<RPSGamePlay>().getMyWins();
        finalScore.text += " PC: " + GameObject.Find("Main CameraG").GetComponent<RPSGamePlay>().getEnemyWins();

    }
}
