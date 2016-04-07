using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndState : StateBase
{
    private static RPSStateManager manager;

    //End state corresponds to final results screen with options to play again or quit
    public EndState(RPSStateManager managerRef)
    {
        manager = managerRef;
        SceneManager.LoadScene("_RockPaperScissors_End");
    }

    public static void NewGame()
    {
        manager.SwitchState(new PlayState(manager));

    }
    public void StateUpdate()
    {
    }
}
