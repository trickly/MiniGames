using UnityEngine;
using System.Collections;

public class RPSContinueButton : MonoBehaviour {

    //used for continuing onto the next round or for switching into the endgame state
	public void NextRound() {
        if (GameObject.Find("Main CameraG").GetComponent<RPSGamePlay>().getGamesPlayed() == 10)
        {
            PlayState.Switch(); //switches to endstate
        }
        else
        {   
            //setsup the game field again
            GameObject.Find("Main CameraG").GetComponent<RPSGamePlay>().showOrHideField(false);
            GameObject.Find("Main CameraG").GetComponent<RPSGamePlay>().showOrHideWeaponMenu(true);
        }
    }
}
