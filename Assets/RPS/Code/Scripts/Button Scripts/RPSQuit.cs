using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RPSQuit : MonoBehaviour {
    //Button script to end the game
    public void quitGame() {
        GameObject.Destroy(GameObject.Find("Main CameraG"));

        GameObject.Find("Main Menu").GetComponent<MainMenu>().TurnMenuBar(true);
        GameObject.Find("Main Menu Canvas").GetComponent<Canvas>().enabled = true;
        SceneManager.LoadScene("_Main_Start");

    }
}
