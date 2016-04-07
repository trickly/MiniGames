using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Associated with the Memory Game
public class RPSConfigurations : MonoBehaviour {
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject confirm;
    private int choice;

    //Sets the currently selected grid size button as green
    void OnEnable()
    {
        choice = Game.current.memSetting;
        confirm.GetComponent<Button>().interactable = false;
        switch (Game.current.memSetting)
        {
            case (3):
                button3.GetComponent<Image>().color = MainMenu.greenColor;
                break;
            case (4):
                button4.GetComponent<Image>().color = MainMenu.greenColor;
                break;
            case (5):
                button5.GetComponent<Image>().color = MainMenu.greenColor;
                break;
        }
    }
    //Resets all the buttons as white
    void OnDisable() {
        button3.GetComponent<Image>().color = Color.white;
        button4.GetComponent<Image>().color = Color.white;
        button5.GetComponent<Image>().color = Color.white;
        confirm.GetComponent<Button>().interactable = false;

    }
    //Confirms the grid size and changes it to the value saved to the account
    public void ConfirmGridSize() {
        Game.current.memSetting = choice;

    }
    //when te user selects the grid size, the colours of the buttons change according to their choice
    public void ChooseGridSize(int x) {
        choice = x;
        confirm.GetComponent<Button>().interactable = true;
        switch (x)
        {
            case (3):
                button3.GetComponent<Image>().color = MainMenu.greenColor;
                button4.GetComponent<Image>().color = Color.white;
                button5.GetComponent<Image>().color = Color.white;

                break;
            case (4):
                button4.GetComponent<Image>().color = MainMenu.greenColor;
                button3.GetComponent<Image>().color = Color.white;
                button5.GetComponent<Image>().color = Color.white;

                break;
            case (5):
                button5.GetComponent<Image>().color = MainMenu.greenColor;
                button3.GetComponent<Image>().color = Color.white;
                button4.GetComponent<Image>().color = Color.white;

                break;
        }

    }
}
