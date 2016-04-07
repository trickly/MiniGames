using UnityEngine;
using System.Collections;

public class RPSStateManager : MonoBehaviour {
    //StateManager class used to keep track of state machine, provide overview and organization of current states + game

    private StateBase activeState;
    private static RPSStateManager instanceRef;
	void Start () {
        activeState = new StartState(this);
	}
	
	void Update () {

        if (activeState != null) {
            activeState.StateUpdate();
           // Debug.Log("This object is of type " + activeState); Used to double check on active state
        }
    }

    public void SwitchState(StateBase newState) {
        activeState = newState;
    }
    public StateBase getActiveState() {
        return activeState;
    }
    
}
