using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RPSMyScore: MonoBehaviour
{
    //Used to update my score during the game
    public Text score;
    void Start()
    {
        score = GameObject.Find("MyScore").GetComponent<Text>();
    }
    
    void Update()
    {
        score.text = "" + GameObject.Find("Main CameraG").GetComponent<RPSGamePlay>().getMyWins();
    }
}
