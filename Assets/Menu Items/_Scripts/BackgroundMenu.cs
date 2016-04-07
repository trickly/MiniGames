using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

//Class associated with the Background screen
public class BackgroundMenu : MonoBehaviour
{

    public Dropdown chooseBackground;
    public Button confirm;
    public Button preview;
    public Text text;
    public GameObject main;
    void Start()
    { 
        confirm.interactable = false;
        preview.interactable = false;
    }
    void OnEnable() {
        chooseBackground.value = Game.current.backgroundArt;
    }

    //Allows user to preview choice. does not save the value
    public void PreviewBackground()
    {
        main.GetComponent<MainMenu>().BackgroundPreview(chooseBackground.value);
        confirm.interactable = true;
    }
    void OnDisable() {
        main.GetComponent<MainMenu>().SetBackground();

    }
    //Activates whenever dropdown menu is changed to allow preview
    public void ChangeChoice() {
        preview.interactable = true;
    }

    //Sets the user background
    public void Confirm()
    {
        confirm.interactable = false;
        main.GetComponent<MainMenu>().BackgroundChange(chooseBackground.value);
        main.GetComponent<MainMenu>().SetBackground();

        text.text = "Background Changed. Choose a new background.";
        

    }
}
