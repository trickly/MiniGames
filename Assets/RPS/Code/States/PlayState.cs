using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayState : StateBase {
    private static RPSStateManager manager;

    //Play state corresponds to the game itself (the weapon menu and the game field)
    public PlayState (RPSStateManager managerRef) {
        //Start the game log at the moment the user enters the game screen

        manager = managerRef;
        Game.current.rpsHistory.Add(new GameLog());
        Game.current.rpsHistory[Game.current.rpsHistory.Count - 1].startTime = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");


        SceneManager.LoadScene("_RockPaperScissors_Game");
    }
    
    public void StateUpdate () {
	    
	}

    public static void Switch()
    {
        //Add onto the Game log of the current playthrough. 
        //Activated whenever the user goes back to the main menu
        //Menu-based restart will count as another playthrough

        //score achieved

        //Finds the number of seconds played by subtracting the end game time with the login time and converting it into seconds
        //login time is also parsed!
        Game.current.rpsHistory[Game.current.rpsHistory.Count - 1].secondsPlayed = (int)(float)System.DateTime.Now.Subtract(System.DateTime.Parse(Game.current.rpsHistory[Game.current.rpsHistory.Count - 1].startTime)).TotalSeconds;

        manager.SwitchState(new EndState(manager));
    }

}
