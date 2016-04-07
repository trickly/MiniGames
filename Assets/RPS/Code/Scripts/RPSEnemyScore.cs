using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RPSEnemyScore : MonoBehaviour
{
    //Updates the Opponent's score during the game
    public Text score;
    void Start()
    {

        score = GameObject.Find("EnemyScore").GetComponent<Text>();
    }
    
    void Update()
    {
        score.text = "" + GameObject.Find("Main CameraG").GetComponent<RPSGamePlay>().getEnemyWins();
        
    }

}
