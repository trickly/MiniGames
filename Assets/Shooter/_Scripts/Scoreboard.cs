using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {

    public int enemyScore;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Text>().text = enemyScore.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
