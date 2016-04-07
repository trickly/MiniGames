using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ApplePickerHighScore : MonoBehaviour {

    public Text scoreText;

	// Use this for initialization
	void Start () {
        GameObject s = GameObject.Find("HighScore");
        scoreText = s.GetComponent<Text>();
        scoreText.text = "High Score: " + Game.current.apHighScore;
        // gt.text = score.ToString();

    }
    void Awake() {
       
        //Assign the high score to ApplePickerHighScore

    }
    // Update is called once per frame
    void Update () {      
        scoreText.text = "High Score: " + Game.current.apHighScore;

    }
}
