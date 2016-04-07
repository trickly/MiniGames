using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartState : StateBase {

    private static RPSStateManager manager;

    //Start State corresponds to the initial starting screen
	public StartState (RPSStateManager managerRef) {
        manager = managerRef;
    }
    
    public void StateUpdate () {
           
	}

    public static void Switch()
    {
        manager.SwitchState(new PlayState(manager));
    }
}
