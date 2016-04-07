using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Asscociated with the Create User screen
public class UserInputUsername : MonoBehaviour
{

    public string username1 = "";
    public string username2 = "";
    InputField[] inputs;
    public GameObject msgPanel;
    public GameObject main;
    //Many initializations because of the input fields, the message panel and the event listeners
    void Start()
    {
        inputs = gameObject.GetComponentsInChildren<InputField>();
        var eventU1 = new InputField.SubmitEvent();
        inputs[0].onEndEdit = eventU1;
        eventU1.AddListener(SetUsername1);
        inputs[1].interactable = false;
        msgPanel.GetComponent<Image>().enabled = false;
        msgPanel.GetComponentInChildren<Text>().text = "";
    }
    
    //sets the username and allows input for the second input field
    public void SetUsername1(string s)
    {
        username1 = s;
        inputs[1].interactable = true;
        var eventU2 = new InputField.SubmitEvent();
        inputs[1].onEndEdit = eventU2;
        eventU2.AddListener(SetUsername2);
    }
    //Input field of the second username
    public void SetUsername2(string s)
    {
        username2 = s;
    }
    //Simply resets thte values of the input fields when exiting screen
    void OnDisable()
    {
        InputField[] inputs = gameObject.GetComponentsInChildren<InputField>();
        inputs[0].text = "";
        inputs[1].text = "";
        main.GetComponent<MainMenu>().msgPanel.GetComponent<Image>().enabled = false;
        main.GetComponent<MainMenu>().msg = "";
        main.GetComponent<MainMenu>().msgPanel.GetComponentInChildren<Text>().text = main.GetComponent<MainMenu>().msg;


    }

    //CHecks if the two entered usernames match and shows a message accordingly

    public void CheckUsername()
    {
        msgPanel.GetComponent<Image>().enabled = true;
        
        //checks if both input fields match
        if (username1 == username2)
        {   //checks if the game dictionary already has the username
            if (SaveLoad.gameDictionary.ContainsKey(username1))
            {
                msgPanel.GetComponent<Image>().color = MainMenu.redColor;
                msgPanel.GetComponentInChildren<Text>().text = "Username already exists.";
            }
            else
            {
                //Success. Creates a new game account
                msgPanel.GetComponent<Image>().color = MainMenu.greenColor;
                msgPanel.GetComponentInChildren<Text>().text = "Account successfully created.";
                Game newGame = new Game(username1, username1);
                SaveLoad.savedGames.Add(newGame);
                SaveLoad.gameDictionary.Add(username1, newGame);
                GameObject.Find("Submit").SetActive(false);

            }

        }
        else
        {
            msgPanel.GetComponent<Image>().color = MainMenu.redColor;
            msgPanel.GetComponentInChildren<Text>().text = "Usernames do not match";
        }

    }
}