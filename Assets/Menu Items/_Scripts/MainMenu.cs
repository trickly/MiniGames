using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

//Main class of the Main Menu and in general, stage 3
//Controls much of the UI interactions and the screen transitions across the game
public class MainMenu : MonoBehaviour {
    public static Color32 greenColor = new Color32(35, 175, 0, 180); //Colours used for message panels across the game screens
    public static Color32 redColor = new Color32(255, 97, 97, 151);



    public GameObject[] screens;
    public string msg;
     int currentScreenIndex;
     int newScreenIndex;

    public GameObject mainMenu;
    public GameObject menuBar;
    public GameObject msgPanel;
    public GameObject[] AudioSourcesPrefabs;
    public Sprite[] backgrounds;
    public AudioClip[] music;
    public GameObject menuSource;
    void Awake()
    {
        //If the saved data has already been loaded, do not load
        if (!SaveLoad.loaded)
        {
            SaveLoad.LoadGame();
            SaveLoad.loaded = true;
        }
        DontDestroyOnLoad(gameObject); //keeps the main menu object alive between scenes
        if (Game.current == null)
        {
        }
        else
        {
            Destroy(gameObject); //destroys new main menus when popping up
        }

    }
    //Used by all children canvases of the main menu in between transitions
    public void PlayMenuSound() {
        menuSource.GetComponent<AudioSource>().Play();
    }
    // Use this for initialization
    void Start()

    {

        if (object.ReferenceEquals(null,Game.current))
        {   //for when the user has not logged in yet
            msgPanel.GetComponent<Image>().enabled = false;
            msg = "";
            msgPanel.GetComponentInChildren<Text>().text = msg;
            currentScreenIndex = 1;
            SetBackgroundLogin();
        }
        else {
            ChangeScreen(0); // if the user is already logged in, head to the home screen
        }
    }

    //Changes the screen based on an index set to each screen/canvas
    //keeps track of previous canvas so it can be set unactive
    public void ChangeScreen(int num){

        //When the user is trying to acces admin only pages without being an admin, not responsive
        if (!((num == 3 || num == 4 || num == 6) && !Game.current.admin)) {
            newScreenIndex = num;
            screens[currentScreenIndex].SetActive(false);
            screens[newScreenIndex].SetActive(true);
            currentScreenIndex = newScreenIndex;

            //makes sure that the menu bar is present only when the user has logged in
            if (currentScreenIndex == 1 || currentScreenIndex == 2)
            {
                menuBar.SetActive(false);
            }
            else
            {
                menuBar.SetActive(true);
            }
        }
    }
    
    //Checks if username + password are matching with any others in the system
    public void LoginCheck() {
        Game gameTemp = null;
        bool success = false;
       //Checks if username matches anything in dictionary
        if (SaveLoad.gameDictionary.TryGetValue(GameObject.Find("Log In Canvas").GetComponent<UserInput>().username, out gameTemp))
        {
            Game.current = SaveLoad.savedGames.Find(p => { return p.username == gameTemp.username; });
            //checks if user account is blocked
            if (!Game.current.blocked)
            {
                //checks if password is correct
                if (GameObject.Find("Log In Canvas").GetComponent<UserInput>().password == Game.current.password)
                {

                    //Matched inputted user with one in system
                    //Load game information of this inputted user
                    success = true;
                    msg = "";
                    msgPanel.GetComponent<Image>().enabled = false;
                    
                }
                else
                {
                    Game.current.loginCounter++;
                    //Username doesn't match password
                    msg = "ERROR: Login Failed. Username does not match password.";

                    print("ERROR: Login Failed. Username does not match password.");
                    if (Game.current.loginCounter == 3 && !Game.current.admin)
                    {
                        Game.current.blocked = true;
                        msg = "ERROR: User is blocked. Request Admin to unblock account.";
                        
                    }


                }
            }
            else{
                //User is blocked
                if (Game.current.loginCounter == 3 && !Game.current.admin)
                {
                    Game.current.blocked = true;
                    msg = "ERROR: User is blocked. Request Admin to unblock account.";
                    
                }

            }
        }
        else {
            //No match with inputted user and system
            msg = "ERROR: Login Failed. No such username exists in the system.";
        }
        msgPanel.GetComponent<Image>().enabled = true;

        if (success)
        {   //If user has logged in successfully
            msgPanel.GetComponent<Image>().color = greenColor;
            msg = "Login Successful.";
            msgPanel.GetComponentInChildren<Text>().text = msg;

            Game.current.loginHistory.Add(new Log()); //Adds a new Log in this user's account
            Game.current.loginHistory[Game.current.loginHistory.Count - 1].logInTime = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); //Sets the LOG IN time

            GameObject.Find("Background Music").GetComponent<AudioSource>().clip = music[Game.current.backgroundMusic]; //Changes the backgorund music
            GameObject.Find("Background Music").GetComponent<AudioSource>().volume = Game.current.backgroundMusicVol; //Changes the background vol
            GameObject.Find("Background Music").GetComponent<AudioSource>().Play(); //plays background music

            //Sets up other audio sources for other scenes
            GameObject.Find("Background Music Source").GetComponent<Audio_Sources>().SetUp();
            GameObject.Find("Winning Music Source").GetComponent<Audio_Sources>().SetUp();
            GameObject.Find("Game Sounds Source").GetComponent<Audio_Sources>().SetUp();
            SetBackground();//sets the background to the user's values

            //Checks if the current logged in user has logged in before
            if (Game.current.firstLogin)   
            {
                //first Time log in
                ChangeScreen(2);
            }
            else {
                //Normal Log in
                ChangeScreen(0);
            }

        }
        else {
            //if user has not logged in successfully
            msgPanel.GetComponentInChildren<Text>().text = msg;
            msgPanel.GetComponent<Image>().color = redColor;
        }

    }
    
    //Changes background based on given index
    public void  BackgroundChange(int index) {
        Game.current.backgroundArt = index;
    }
    //Sets the background to the loaded value
    public void SetBackground() {
        mainMenu.GetComponentInChildren<Image>().sprite = backgrounds[Game.current.backgroundArt];
    }
    //Used only for the login screen to set up the screen for that
    public void SetBackgroundLogin()
    {
        mainMenu.GetComponentInChildren<Image>().sprite = backgrounds[3];
    }
    //Background Preview is used to temporarily set up the image sprite of the main menu
    public void BackgroundPreview(int x)
    {
        mainMenu.GetComponentInChildren<Image>().sprite = backgrounds[x];
    }
    //Enters a new scene for the game
    public void PlayGame(string name) {
        TurnMenuBar(false);
        AudioSourcesPrefabs[3].GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene(name);
    }

    //Used to make the menu bar uninteractable
    public void TurnMenuBar(bool on) {
        foreach (Button b in menuBar.GetComponentsInChildren<Button>(true))
        {
            b.interactable = on;
        }
    }

    //Saves and then exits the program
    //Made a function for this because using the inspector to attach 2 functions makes it unsure which function to launch first
    public void SaveAndExit()
    {
        SaveLoad.Save();
        ExitGame();
    }
    public void ExitGame() {
        Application.Quit();
    }

    //Saves and then logsout of the program
    //Made a function for this because using the inspector to attach 2 functions makes it unsure which function to launch first
    public void SaveAndLogOut()
    {
        SaveLoad.Save();
        LogOut();
    }
    public void LogOut()
    {
        ChangeScreen(1);
        Game.current = null;
    }

}
