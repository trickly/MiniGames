using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RPSPlayAgain : MonoBehaviour {
    //Button script to restart the game
    public void restartGame() {
        GameObject.Destroy(GameObject.Find("Main CameraG"));

        EndState.NewGame();
    }
}
