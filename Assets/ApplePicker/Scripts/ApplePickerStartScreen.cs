using UnityEngine;
using UnityEngine.SceneManagement;
public class ApplePickerStartScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        foreach (Canvas a in GameObject.Find("Main Menu").GetComponentsInChildren<Canvas>(true))
        {
            a.worldCamera = Camera.main;
            a.planeDistance = 1;
        }
        GameObject.Find("Main Menu Canvas").GetComponent<Canvas>().planeDistance = 0; 
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void StartLevel() {
        //Start the game log at the moment the user enters the game screen
        Game.current.appleHistory.Add(new GameLog());
        Game.current.appleHistory[Game.current.appleHistory.Count - 1].startTime = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        SceneManager.LoadScene("_ApplePicker_Game");
    }
    public void ExitApple() {
        // GameObject.Find("Main Menu").
        GameObject.Find("Main Menu Canvas").GetComponent<Canvas>().planeDistance = 1;
        GameObject.Find("Main Menu").GetComponent<MainMenu>().TurnMenuBar(true);
        SceneManager.LoadScene("_Main_Start");

    }
}
