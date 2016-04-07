using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MemoryGame_Gameplay: MonoBehaviour
{
    public static string[] rapperNames;
    public static bool[] rapperChosen;
    public static int numCardsX;
    public static int numCardsY;


    public GameObject[] rappers;
    private string card1, card2;
    GameObject card1Object, card2Object;
    int numOfRappers;
    int score;
    int rappersLeft;
    private string rapperName;
    public static bool inputAllowed;

    // Use this for initialization
    void Start()
    {

        score = 1000;
        SetUpCards();
        numOfRappers = numCardsX * numCardsY / 2;
        rappersLeft = numOfRappers;
        inputAllowed = true;
        
        
        //Sets up the camera for the menu bar
        foreach (Canvas a in GameObject.Find("Main Menu").GetComponentsInChildren<Canvas>(true))
        {
            a.worldCamera = Camera.main;
           // a.planeDistance = 1;
        }

    }
    // Update is called once per frame
    void Update()
    {

        GameObject.Find("Time").GetComponent<Text>().text = (((int)Time.timeSinceLevelLoad / 60).ToString() + ":" + (((int)Time.timeSinceLevelLoad % 60) / 10).ToString() + (((int)Time.timeSinceLevelLoad % 60) % 10).ToString());

        GameObject.Find("Score").GetComponent<Text>().text = "Score: " + score;

            if (Input.GetMouseButtonDown(0) && inputAllowed)
            { // if left button pressed...
                PointerEventData pointer = new PointerEventData(EventSystem.current);
                pointer.position = Input.mousePosition;

                List<RaycastResult> raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointer, raycastResults);

                if (raycastResults.Count > 0)
                {
                    if (raycastResults[0].gameObject.layer != 2)
                    {


                    CheckIfRapper(raycastResults[0].gameObject.tag);

                        if (card1 == null)
                        {
                            GameObject.Find("Audio Source").GetComponent<AudioSource>().Play();

                            card1Object = raycastResults[0].gameObject;
                            card1 = raycastResults[0].gameObject.tag;
                            inputAllowed = true;
                            Turn(card1Object);

                    }
                    else
                        {
                            GameObject.Find("Audio Source").GetComponent<AudioSource>().Play();
                            card2Object = raycastResults[0].gameObject;
                            card2 = raycastResults[0].gameObject.tag;
                            Turn(card2Object);
                            StartCoroutine(DelayNextRound());
                            CheckGameResult();

                        }
                    }
                }
            }
       

    }
    private void CheckGameResult()
    {

        if (rappersLeft == 0)
        {

            foreach (AudioSource clip in GameObject.Find("Audio Source").GetComponents<AudioSource>())
            {
                if (clip.clip.name == "Win")
                {
                    clip.Play();
                }

            }
            //win
            Time.timeScale = 0;
            GameObject.Find("EndGame").GetComponent<Text>().text = "YOU WON!";
            DelayedRestart(3f);

        }
        if (score == 0)
        {

            //lose
            foreach (AudioSource clip in GameObject.Find("Audio Source").GetComponents<AudioSource>())
            {
                if (clip.clip.name == "Lose")
                {
                    clip.Play();
                }

            }
            Time.timeScale = 0;
            GameObject.Find("EndGame").GetComponent<Text>().text = "GAME OVER";
            DelayedRestart(3f);


        }


    }
    private void CheckIfRapper(string tag)
    {
        for (int i = 0; i < numOfRappers; i++)
        {

            if (tag == rapperNames[i]) //match
            {
                rapperName = rapperNames[i];

            }
        }
    }
    private bool CheckRapper()
    {

        if (card1 == card2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetUpCards()
    {
        //Checks what setting the user has set up for the number of cards to appear. Default is 4 x 3
        if (Game.current.memSetting == 3)
        {
            numCardsX = 3;
            numCardsY = 4;
            rapperChosen = new bool[12];
            rapperNames = new string[12] { "Drake", "Eminem", "Jay Z", "Kanye West", "Kendrick Lamar", "Lil Wayne",
                                             "Drake", "Eminem", "Jay Z","Kanye West", "Kendrick Lamar", "Lil Wayne"};

        }
        else if (Game.current.memSetting == 4)
        {
            numCardsX = 4;
            numCardsY = 4;
            rapperChosen = new bool[16];
            rapperNames = new string[16] { "Drake","Eminem", "Future", "Jay Z", "J. Cole", "Kanye West", "Kendrick Lamar", "Lil Wayne",
                                             "Drake","Eminem", "Future", "Jay Z", "J. Cole", "Kanye West", "Kendrick Lamar",  "Lil Wayne"};

        }
        else {
            numCardsX = 5;
            numCardsY = 4;
            rapperChosen = new bool[20];
            rapperNames = new string[20] { "Drake", "Chilidsh Gambino", "Eminem", "Future", "Jay Z", "J. Cole", "Kanye West", "Kendrick Lamar", "Kid Cudi", "Lil Wayne",
                                             "Drake", "Chilidsh Gambino", "Eminem", "Future", "Jay Z", "J. Cole", "Kanye West", "Kendrick Lamar", "Kid Cudi", "Lil Wayne"};

        }
        for (int i = 0; i < numCardsX; i++)
        {
            for (int j = 0; j < numCardsY; j++)
            {
                int ndx;
                bool pass = false;
                do
                {
                    ndx = UnityEngine.Random.Range(0, rapperChosen.Length);
                    if (rapperChosen[ndx] != true)
                    {
                        rapperChosen[ndx] = true;
                        pass = true;
                    }
                } while (pass == false);
                GameObject go = Instantiate(rappers[ndx]) as GameObject;
                go.transform.parent = GameObject.Find("Canvas").transform;
                go.transform.localScale = new Vector3(1, 1, 1);
                go.transform.localPosition = new Vector3(-316 + i * 80, 213 - j * 120 -60, 0);

                //go.transform.localPosition = new Vector3(-316 + i * 100, 213 - j * 145, 0);
            }

        }
    }

    private void Turn(GameObject card) {

        if (MemoryGame_Gameplay.inputAllowed == true)
        {
            bool a = false;

            foreach (Transform d in card.transform.parent.gameObject.GetComponentsInChildren<Transform>())
            {
                if (a == false)
                {
                    a = true;
                }
                else
                {
                    d.Rotate(new Vector3(0, 90, 0));

                }
            }
        }
       


    }

    IEnumerator DelayNextRound()
    {
        inputAllowed = false;
        yield return new WaitForSeconds(1);
        if (CheckRapper())
        {//if there is a match
            foreach (AudioSource clip in GameObject.Find("Audio Source").GetComponents<AudioSource>())
            {
                if (clip.clip.name == "Right")
                {
                    clip.Play();
                }

            }
            bool a = false;
            foreach (Transform d in card1Object.transform.parent.gameObject.GetComponentsInChildren<Transform>())
            {
                if (a == false)
                {
                    a = true;
                }
                else
                {
                    d.parent.gameObject.SetActive(false);
                }
            }
            bool b = false;

            foreach (Transform e in card2Object.transform.parent.gameObject.GetComponentsInChildren<Transform>())
            {
                if (b == false)
                {
                    b = true;
                }
                else
                {
                    e.parent.gameObject.SetActive(false);
                }
            }

            card1Object = null;
            card1 = null;
            card2Object = null;
            card2 = null;
            rappersLeft--;
            CheckGameResult();
        }
        else
        {
            foreach (AudioSource clip in GameObject.Find("Audio Source").GetComponents<AudioSource>())
            {
                if (clip.clip.name == "Wrong")
                {
                    clip.Play();
                }

            }
            //if there isnt a match
            bool a = false;
            if (card2Object != null)
            {
                foreach (Transform d in card1Object.transform.parent.gameObject.GetComponentsInChildren<Transform>())
                {
                    if (a == false)
                    {
                        a = true;
                    }
                    else
                    {
                        d.Rotate(new Vector3(0, -90, 0));
                    }
                }
                bool b = false;

                foreach (Transform e in card2Object.transform.parent.gameObject.GetComponentsInChildren<Transform>())
                {
                    if (b == false)
                    {
                        b = true;
                    }
                    else
                    {
                        e.Rotate(new Vector3(0, -90, 0));
                    }
                }
                card1 = null;
                card1Object = null;
                card2Object = null;
                card2 = null;
                score -= 40;
            }
            CheckGameResult();
        }
        inputAllowed = true;

    }

    //Start Screen now
    public void ToMenuScreen()
    {           
        //Add onto the Game log of the current playthrough. 
                 
        //Activated whenever the user goes back to the main menu
                 
        //Menu-based restart will count as another playthrough

        //score achieved
        Game.current.memoryHistory[Game.current.memoryHistory.Count - 1].score = score;

        //Finds the number of seconds played by subtracting the end game time with the login time and converting it into seconds
        //login time is also parsed!
        Game.current.memoryHistory[Game.current.memoryHistory.Count - 1].secondsPlayed = (int)(float)System.DateTime.Now.Subtract(System.DateTime.Parse(Game.current.memoryHistory[Game.current.memoryHistory.Count - 1].startTime)).TotalSeconds;

        Time.timeScale = 1;
        SceneManager.LoadScene("_MemoryGame_Start");

    }
    public void DelayedRestart(float delay)
    {
        GameObject.Find("Menu Options").GetComponent<Image>().enabled = true;
        Invoke("ToMenuScreen", delay);
    }
}
