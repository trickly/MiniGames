using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Class associated with the Delete User screen

public class DeleteUser : MonoBehaviour {
    
    int index;
    public Dropdown chooseUser;
    public Button confirm;
    public GameObject yes;
    public GameObject no;
    public Text text;

    // Use this for initialization
    void Start() {
        
        chooseUser.ClearOptions(); //clears options when starting up
        confirm.interactable = false;
        yes.SetActive(false);
        no.SetActive(false);
        for (int i = 0; i < SaveLoad.savedGames.Count; i++) {
            chooseUser.options.Add(new Dropdown.OptionData("Username: " + SaveLoad.savedGames[i].username)); //Instantiates new option for the dropdown baseed on prefab
        }

      
    }
    //Allows admin to confirm choice
    public void SetAccount() {

        confirm.interactable = true;
    }
    //Makes sure the user is double confirmed
    public void Confirm() {
        text.text = "Are you sure?";
        chooseUser.interactable = false;
        yes.SetActive(true);
        no.SetActive(true);
        confirm.gameObject.SetActive (false); 
        
    }
    //Confirms the choice of deletion
    public void Yes()
    {
        chooseUser.interactable = true;
        confirm.gameObject.SetActive(true);

        confirm.interactable = false;
        yes.SetActive(false);
        no.SetActive(false);
        if (SaveLoad.savedGames[chooseUser.value].username == "admin")
        {
            text.text = "'admin' cannot be deleted. Choose another user.";
        }
        else {
            text.color = Color.white;
            text.text = "Account deleted. Choose another user.";

            SaveLoad.gameDictionary.Remove(SaveLoad.savedGames[chooseUser.value].username); //removes user from game dictionary
            SaveLoad.savedGames.RemoveAt(chooseUser.value); // Removes user from account
            ResetList();
        }
        

    }
    //When the user does not want to confirm choices. reverts everything back to normal
    public void No()
    {
        text.text = "Choose a user from the list.";
        chooseUser.interactable = true;
        yes.SetActive(false);
        no.SetActive(false);
        confirm.gameObject.SetActive(true);

    }

    //Reset List is used whenever the need to clear and reload data is needed
    void ResetList() {
        chooseUser.ClearOptions();
        for (int i = 0; i < SaveLoad.savedGames.Count; i++)
        {
            chooseUser.options.Add(new Dropdown.OptionData("Username: " + SaveLoad.savedGames[i].username));
        }

    }

    //Whenever going back to the screen, need to follow similar procedures as Start()
    void OnEnable () {
        chooseUser.ClearOptions();
        confirm.interactable = false;
        yes.SetActive(false);
        no.SetActive(false);
        for (int i = 0; i < SaveLoad.savedGames.Count; i++)
        {
            chooseUser.options.Add(new Dropdown.OptionData("Username: " + SaveLoad.savedGames[i].username));
        }

    }
}
