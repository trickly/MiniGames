using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Background : MonoBehaviour
{
    Dropdown drop;
    private int currentLevelBackground;
    public Button confirm;
    public Button preview;
    public Sprite[] ssBackgrounds;
    Resolution[] resolutions;
    public GameObject resolutionButtonPrefab;
    public GameObject dropdown;
    public GameObject main;
    // Use this for initialization
    void Start()
    {
        confirm.interactable = false;
        preview.interactable = false;

        drop = gameObject.GetComponentInChildren<Dropdown>();
        drop.onValueChanged.AddListener(delegate
        {
            levelChange(drop);
        });
        enableButton(false);

        //Resolutions Code
        dropdown.GetComponent<Dropdown>().ClearOptions();
        resolutions = new Resolution[2];
        // Screen.resolutions;
        resolutions[0].height = 800;
        resolutions[0].width = 600;
        resolutions[1].height = 1800;
        resolutions[1].width = 3200;

        for (int i = 0; i < resolutions.Length; i++) {
            dropdown.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));
        }
    }

    void OnEnable()
    {
        returnFromConfiguration();
    }
    
    void OnDisable()
    {
        confirm.interactable = false;
        preview.interactable = false;
        main.GetComponentInChildren<MainMenu>().SetBackground();//this has to happen whenever this leaves

    }

    //Changes the resolution based on the dropdown value and resolution index
    public void SetResolution() {
        ChangeResolution(dropdown.GetComponent<Dropdown>().value);
    }
    public void ChangeResolution(int index) {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, false);
       // Screen.SetResolution(800, 600, false);
    }

    //ToString function to simplify the Resolution object to an easier to understand statemnt
    string ResToString(Resolution res) {
        return res.width + " x " + res.height;
    }
    //Dropdown function that changes the background of each level based on the index
    private void levelChange(Dropdown target)
    {
        switch (target.value)
        {
            case 0://None Selected
                enableButton(false);
                break;
            case 1://Bronze
                enableButton(true);
                currentLevelBackground = Game.current.shooterSettings.backgroundB;
                break;
            case 2://Silver
                enableButton(true);
                currentLevelBackground = Game.current.shooterSettings.backgroundS;
                break;
            case 3://Gold
                enableButton(true);
                currentLevelBackground = Game.current.shooterSettings.backgroundG;
                break;
            case 4://Gold
                enableButton(true);
                currentLevelBackground = Game.current.shooterSettings.background;
                break;
        }
        setSelected();
    }

    //based on the value of the currently selected background, the indicator also moves with it
    private void setSelected()
    {
        switch (currentLevelBackground) {
            case 0:
                break;
            case 1:
                leftSelected();
                break;
            case 2:
                midSelected();
                break;
            case 3:
                rightSelected();
                break;
        }
    }
    //Used for Aesthetic purposes. Enables the images/buttons of the wallpapers
    private void enableButton(bool active)
    {
        GameObject.Find("Background Choice").GetComponent<Image>().enabled = active;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Background Image"))
        {
            go.GetComponent<Button>().interactable = active;
        }
    }

    //Used for whenever the values need to be reset
    public void returnFromConfiguration()
    {
        enableButton(false);
        if (drop != null)
        {
            drop.value = 0;
        }
    }
    //SAVE Function. save the value of background index based on the dropdown's value
    private void saveSelection() {
        switch (drop.value) {
            case 0:
                break;
            case 1:
                Game.current.shooterSettings.backgroundB = currentLevelBackground;
                break;

            case 2:
                Game.current.shooterSettings.backgroundS = currentLevelBackground;
                break;

            case 3:
                Game.current.shooterSettings.backgroundG = currentLevelBackground;
                break;
            case 4:
                Game.current.shooterSettings.background = currentLevelBackground;
                break;

        }

    }

    //Allows user to preview choice
    public void PreviewBackground()
    {
        GameObject.Find("Main Menu Canvas").GetComponentInChildren<Image>().sprite = ssBackgrounds[currentLevelBackground - 1];
        confirm.interactable = true;
    }
    //Confirms decision. changes certain interactable buttons
    public void Confirm()
    {
        confirm.interactable = false;
        preview.interactable = false;
        GameObject.Find("Main Menu").GetComponentInChildren<MainMenu>().SetBackground();//this has to happen whenever this leaves
        saveSelection();
    }
    

    //moves the indicator to the left
    public void leftSelected()
    {
        GameObject.Find("Background Choice").GetComponent<Image>().transform.position = GameObject.Find("Background Image 1").GetComponent<Image>().transform.position;
        currentLevelBackground = 1;
        confirm.interactable = true;
        preview.interactable = true;

    }

    //moves the indicator to the middle
    public void midSelected()
    {
        GameObject.Find("Background Choice").GetComponent<Image>().transform.position = GameObject.Find("Background Image 2").GetComponent<Image>().transform.position;
        currentLevelBackground = 2;
        preview.interactable = true;

        confirm.interactable = true;
    }

    //moves the indicator to the right
    public void rightSelected()
    {
        GameObject.Find("Background Choice").GetComponent<Image>().transform.position = GameObject.Find("Background Image 3").GetComponent<Image>().transform.position;
        currentLevelBackground = 3;
        confirm.interactable = true;
        preview.interactable = true;


    }


}
