using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RPSScoreBoard : MonoBehaviour {
    //used during the game to keep track of the number of rounds
    public Text rounds;
	void Start () {

        rounds = GameObject.Find("RoundNumber").GetComponent<Text>();
	}
	
	void Update () {
        rounds.text = "" + GameObject.Find("Main CameraG").GetComponent<RPSGamePlay>().getGamesPlayed();
	}
}
