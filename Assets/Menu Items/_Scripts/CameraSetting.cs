using UnityEngine;
using System.Collections;

//Class associated with the Main camera in the _Main_Start screen
//makes sure that when the GameObject of the main menu returns back to that scene, the world camera of each canvas is set to this GameObject
public class CameraSetting : MonoBehaviour {

	// Use this for initialization
	void Start () {
        foreach (Canvas a in GameObject.Find("Main Menu").GetComponentsInChildren<Canvas>(true))
        {
            a.worldCamera = Camera.main;
        }
    }
}
