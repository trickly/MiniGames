using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MemoryGame_Start_Screen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Sets up the camera for the menu bar
        foreach (Canvas a in GameObject.Find("Main Menu").GetComponentsInChildren<Canvas>(true))
        {
            a.worldCamera = Camera.main;
           // a.planeDistance = 1;
        }

    }
    
    public void PlaySound() {
        GameObject.Find("Audio Source").GetComponent<AudioSource>().Play();
    }
    public void StartGame() {
        GameObject.Find("Main Menu").GetComponent<MainMenu>().TurnMenuBar(false);
        GameObject.Find("Main Menu Canvas").GetComponent<Canvas>().enabled = false;

        Game.current.memoryHistory.Add(new GameLog());
        Game.current.memoryHistory[Game.current.memoryHistory.Count - 1].startTime = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        SceneManager.LoadScene("_MemoryGame_Game");
    }
    public void ExitGame()
    {
        GameObject.Find("Main Menu Canvas").GetComponent<Canvas>().enabled = true;

        //GameObject.Find("Main Menu Canvas").GetComponent<Canvas>().planeDistance = 1;
        GameObject.Find("Main Menu").GetComponent<MainMenu>().TurnMenuBar(true);
        SceneManager.LoadScene("_Main_Start");
    }
}//        DontDestroyOnLoad(gameObject);

