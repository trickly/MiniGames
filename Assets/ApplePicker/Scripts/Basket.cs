using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Basket : MonoBehaviour {

    public Text scoreGT;
    static public int scoreCurrent;
	// Use this for initialization
	void Start () {
        //Find a reference to the ScoreCounter GameObject
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        //Get the GUIText component of that GameObject
        scoreGT = scoreGO.GetComponent<Text>();
        scoreGT.text = "Score: " + scoreCurrent;
    }
	
	// Update is called once per frame
	void Update () {
        //Get the current screen position of the mouse from Input
        Vector3 mousePos2D = Input.mousePosition;
        //Camera's Z position sets how far to push the mouse into 3D
        mousePos2D.z = -Camera.main.transform.position.z;
        //Convert the point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        //Move the x position fo this basket to th epositon of the mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
        scoreGT.text = "Score: " + scoreCurrent;

    }

    void OnCollisionEnter(Collision coll) {
        //Find out what hit this basket
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.tag == "Apple")
        {
            Destroy(collidedWith);
        }
        //Parse the text of the scoreGT into an int
        //score = int.Parse(scoreGT.text);
        //add points for catching apple
        scoreCurrent += 100;
        //convert score back to a sting to display it
       //scoreGT.text = scoreCurrent.ToString();

        //high score update
        if (scoreCurrent > Game.current.apHighScore) {
            Game.current.apHighScore = scoreCurrent;
        }
    }
}
