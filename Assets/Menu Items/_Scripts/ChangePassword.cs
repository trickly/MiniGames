using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Class associated with the Change Password screen
public class ChangePassword : MonoBehaviour
{

    public string password1 = "";
    public string password2 = "";
    InputField[] inputs;
    public GameObject msgPanel;
    public GameObject submit;
    public GameObject quit;

    // many initializations for input fields, a message panel, and activating event triggers
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
    //Associated with the second input field where user has to confirm choices
    public void SetPassword2(string s)
    {
        password2 = s;
        submit.GetComponent<Button>().interactable = true;
    }

    //Associated with the Confirm button
    //passwords must match, otherwise an error shows up in the message panel
    public void CheckPassword()
    {
        msgPanel.GetComponent<Image>().enabled = true;
        //checks if both input fields match
        if (password1 == password2)
        {
            //Passwords match            
            submit.SetActive(false);
            quit.SetActive(false);

            msgPanel.GetComponent<Image>().color = MainMenu.greenColor;
            msgPanel.GetComponentInChildren<Text>().text = "Password successfully changed.";
            Game.current.password = password1;
            Game.current.firstLogin = false;


        }
        else
        {   //Passwords do not match
            msgPanel.GetComponent<Image>().color = MainMenu.redColor;
            msgPanel.GetComponentInChildren<Text>().text = "Passwords do not match.";
        }

    }
}
