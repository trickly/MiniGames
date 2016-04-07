using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Associated with the Log In First Time Screen 
public class UserInputPassword : MonoBehaviour {

    public string password1 = "";
    public string password2 = "";
    InputField[] inputs;
    public GameObject msgPanel;
    public GameObject submit;
    public GameObject quit;
    public GameObject home;
    public GameObject main;
    //Many initializations because of the input fields, the message panel and the event listeners
    void Start()
    {

        inputs = gameObject.GetComponentsInChildren<InputField>();
        var eventP1 = new InputField.SubmitEvent();
        inputs[0].onEndEdit = eventP1;
        eventP1.AddListener(SetPassword1);
        inputs[1].interactable = false;
        msgPanel.GetComponent<Image>().enabled = false;
        msgPanel.GetComponentInChildren<Text>().text = "";
        submit.GetComponent<Button>().interactable = false;
        
        home.SetActive(false);
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
    //sets the password and allows input for the second input field
    public void SetPassword1(string s)
    {
        password1 = s;
        inputs[1].interactable = true;
        var eventP2 = new InputField.SubmitEvent();
        inputs[1].onEndEdit = eventP2;
        eventP2.AddListener(SetPassword2);
    }
    //input field for the second password
    public void SetPassword2(string s)
    {
        password2 = s;
        submit.GetComponent<Button>().interactable = true;
    }

    //CHecks if the two entered passwords match and shows a message accordingly
    public void CheckPassword()
    {
        msgPanel.GetComponent<Image>().enabled = true;
        //checks if both input fields match
        if (password1 ==password2)
        {
            //checks if the entered password is the same as the previous one
            if (!(password1 == Game.current.password))
            {
                //checks length of password
                if (password1.Length > 4)
                {

                    //Success            
                    submit.SetActive(false);
                    quit.SetActive(false);
                    home.SetActive(true);
                    msgPanel.GetComponent<Image>().color = MainMenu.greenColor;
                    msgPanel.GetComponentInChildren<Text>().text = "Password successfully changed.";
                    Game.current.password = password1;
                    Game.current.firstLogin = false;


                }
                else {
                    msgPanel.GetComponent<Image>().color = MainMenu.redColor;
                    msgPanel.GetComponentInChildren<Text>().text = "Password must be more than 4 characters.";
                }

            }
            else {
                msgPanel.GetComponent<Image>().color = MainMenu.redColor;
                msgPanel.GetComponentInChildren<Text>().text = "Don't use the same password!";
            }


        }
        else {
            msgPanel.GetComponent<Image>().color = MainMenu.redColor;
            msgPanel.GetComponentInChildren<Text>().text = "Passwords do not match.";
        }

    }
}
