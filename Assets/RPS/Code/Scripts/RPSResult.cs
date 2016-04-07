using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RPSResult : MonoBehaviour
{
    //Used for the endgame screen to show final results
    public Text result;
    void Start()
    {
        foreach (Canvas a in GameObject.Find("Main Menu").GetComponentsInChildren<Canvas>(true))
        {
            a.worldCamera = Camera.main;
            //a.planeDistance = 1;
        }
        result = GameObject.Find("Result").GetComponent<Text>();
        result.text = "" + determineWinner();

    }
    public string determineWinner()
    {
        if (GameObject.Find("Main CameraG").GetComponent<RPSGamePlay>().getMyWins() == GameObject.Find("Main CameraG").GetComponent<RPSGamePlay>().getEnemyWins())
        {
            Game.current.rpsHistory[Game.current.rpsHistory.Count - 1].score = 0;
            return "You Tied!";

        }
        else if (GameObject.Find("Main CameraG").GetComponent<RPSGamePlay>().getMyWins() > GameObject.Find("Main CameraG").GetComponent<RPSGamePlay>().getEnemyWins())
        {
            Game.current.rpsHistory[Game.current.rpsHistory.Count - 1].score = 1;

            return "You Won!";

        }
        else
        {
            Game.current.rpsHistory[Game.current.rpsHistory.Count - 1].score = -1;

            return "You Lost!";

        }
    }
}
