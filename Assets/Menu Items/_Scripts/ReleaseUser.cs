using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;


//Class associated with the Release Blocks screen
public class ReleaseUser : MonoBehaviour
{

    int index;
    public Dropdown chooseUser;
    public Button confirm;
    public GameObject yes;
    public GameObject no;
    public Text text;
    List<Game> temp;
    // Use this for initialization
    void Start()
    {
        chooseUser.ClearOptions();
        confirm.interactable = false;
        yes.SetActive(false);
        no.SetActive(false);
        temp = new List<Game>();
        for (int i = 0; i < SaveLoad.savedGames.Count; i++)
        {
            if (SaveLoad.savedGames[i].blocked)
            {
                 chooseUser.options.Add(new Dropdown.OptionData("Username: " + SaveLoad.savedGames[i].username)); // instantitates dropdown options based on users who are blocked
                temp.Add(SaveLoad.savedGames[i]);
            }
        }


    }
    //Allows admin to confirm choice
    public void SetAccount()
    {

        confirm.interactable = true;
    }
    //Makes sure the user is double confirmed
    public void Confirm()
    {
        text.text = "Are you sure?";
        chooseUser.interactable = false;
        yes.SetActive(true);
        no.SetActive(true);
        confirm.gameObject.SetActive(false);

    }
    //When the user chooses to go through with blocking user
    //Reverts other UI settings
    public void Yes()
    {
        text.text = "Account unblocked.";
        chooseUser.interactable = true;
        confirm.gameObject.SetActive(true);
        confirm.interactable = false;
        yes.SetActive(false);
        no.SetActive(false);
        Predicate<Game> gameFinder = p => { return p.username == temp[chooseUser.value].username; };

        int num = SaveLoad.savedGames.FindIndex(gameFinder); //Uses a predicate to find the location of the user to be unblocked
        // Unblock account, set password to username, set status to new
        SaveLoad.savedGames[num].blocked = false;
        SaveLoad.savedGames[num].password = SaveLoad.savedGames[num].username;
        SaveLoad.savedGames[num].firstLogin = true;
        print(SaveLoad.savedGames[num].username);

    }
    //When the user chooses not to go through with unblocking user
    //Reverts other UI settings
    public void No()
    {
        text.text = "Choose a user from the list.";
        chooseUser.interactable = true;
        yes.SetActive(false);
        no.SetActive(false);

    }
    //Similar to the Start()
    void OnEnable()
    {
        chooseUser.ClearOptions();
        confirm.interactable = false;
        yes.SetActive(false);
        no.SetActive(false);
        temp = new List<Game>();
        for (int i = 0; i < SaveLoad.savedGames.Count; i++)
        {
            if (SaveLoad.savedGames[i].blocked)
            {
                chooseUser.options.Add(new Dropdown.OptionData("Username: " + SaveLoad.savedGames[i].username));
                temp.Add(SaveLoad.savedGames[i]);
            }
        }
    }
}
