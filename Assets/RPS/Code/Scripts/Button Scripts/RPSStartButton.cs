using UnityEngine;
using System.Collections;

public class RPSStartButton : MonoBehaviour {
    //button script to start the game from the start screen
    public void Switch() {

        StartState.Switch();
    }
    void Start() {
        GameObject.Find("Main Menu Canvas").GetComponent<Canvas>().enabled = false;

        foreach (Canvas a in GameObject.Find("Main Menu").GetComponentsInChildren<Canvas>(true))
        {
            a.worldCamera = Camera.main;
          //  a.planeDistance = 1;
        }
    }
}
