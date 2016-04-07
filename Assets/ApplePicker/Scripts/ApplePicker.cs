using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApplePicker : MonoBehaviour {
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;
    public GameObject basketPrefab;

    // Use this for initialization
    void Start () {
        foreach (Canvas a in GameObject.Find("Main Menu").GetComponentsInChildren<Canvas>(true))
        {
            a.worldCamera = Camera.main;
            a.planeDistance = 1;
        }
        GameObject.Find("Main Menu Canvas").GetComponent<Canvas>().planeDistance = 0;

        basketList = new List<GameObject>();
        
        for (int i = 0; i < numBaskets; i++) {
            
            GameObject tBasketGO = Instantiate(basketPrefab) as GameObject;
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
           tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }

    }
    public void newGame()
    {
            UnityEngine.SceneManagement.SceneManager.LoadScene("_ApplePicker_Game");
    }
	// Update is called once per frame
	void Update () {
	
	}
    public void AppleDestroyed() {
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tGo in tAppleArray) {
            Destroy(tGo);
        }
        //destroy one of the baskets 
        //get the index of the last basket in basketlist
        int basketIndex = basketList.Count - 1;
        //Get a reference to hat Basket gameobject
        GameObject tBasketGO = basketList[basketIndex];
        //remove the basket from the list and destroy the GameObject
        basketList.RemoveAt(basketIndex);
        Destroy(tBasketGO);

        //WHen player loses
        if (basketList.Count == 0)
        {
            //Add onto the Game log of the current playthrough. 
            //Activated whenever the user goes back to the main menu
            //Menu-based restart will count as another playthrough

            //score achieved
            Game.current.appleHistory[Game.current.appleHistory.Count - 1].score = Basket.scoreCurrent;

            //Finds the number of seconds played by subtracting the end game time with the login time and converting it into seconds
            //login time is also parsed!
            Game.current.appleHistory[Game.current.appleHistory.Count - 1].secondsPlayed = (int)(float)System.DateTime.Now.Subtract(System.DateTime.Parse(Game.current.appleHistory[Game.current.appleHistory.Count - 1].startTime)).TotalSeconds;

            UnityEngine.SceneManagement.SceneManager.LoadScene("_ApplePicker_Start");

        }

    }

}
