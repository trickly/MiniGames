using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Class associated with the Log In screen
public class UserInput : MonoBehaviour {

    public string username ="";
    public string password = "";
    public GameObject main;
    // Use this for initialization
    void Start () {
        InputField[] inputs = gameObject.GetComponentsInChildren<InputField>();
        
        var eventUser = new InputField.SubmitEvent();
        inputs[0].onEndEdit = eventUser;
        eventUser.AddListener(SetUsername);

        var eventPass = new InputField.SubmitEvent();
        inputs[1].onEndEdit = eventPass;
        eventPass.AddListener(SetPassword);

    }
    //Simply resets thte values of the input fields when exiting screen
    void OnDisable() {
        InputField[] inputs = gameObject.GetComponentsInChildren<InputField>();
        inputs[0].text = "";
        inputs[1].text = "";
        main.GetComponent<MainMenu>().msgPanel.GetComponent<Image>().enabled = false;
        main.GetComponent<MainMenu>().msg = "";
        main.GetComponent<MainMenu>().msgPanel.GetComponentInChildren<Text>().text = main.GetComponent<MainMenu>().msg;


    }
    //Sets the username of the screen to check in Main Menu class
    public void SetUsername(string s)
    {
        username = s;

    }
    //Sets the password of the screen to check in Main Menu class
    public void SetPassword(string s) {
        password = s;

    }
}
