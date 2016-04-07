using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Active_Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var tog = gameObject.GetComponent<Toggle>();
        var se = new Toggle.ToggleEvent();
        tog.onValueChanged = se;
        se.AddListener(ifActive);
    }

    //Determines whether or not an enemy is active in the level
    //uses the error panel to make sure that the minimum requirements per level is still reached
    public void ifActive(bool active)
    {
        if (GameObject.Find("Error Panel").GetComponent<Image>().enabled)
        {
            GameObject.Find("Error Panel").GetComponent<Image>().enabled = false;
            GameObject.Find("Error Panel").GetComponentInChildren<Text>().text = "";

        }

        int temp = 0;
        if (active)
        {
            temp = 1;
        }
        else
        {
            temp = 0;
        }

        if (GameObject.Find("Dropdown").GetComponent<Dropdown>().value == 1)
        {
            if (gameObject.name.Equals("Enemy 1 Toggle"))
            {
                Game.current.shooterSettings.enemy1B = temp;

            }
            else if (gameObject.name.Equals("Enemy 2 Toggle"))
            {

                Game.current.shooterSettings.enemy2B = temp;
            }
            else if (gameObject.name.Equals("Enemy 3 Toggle"))
            {

                Game.current.shooterSettings.enemy3B = temp;
            }
            else if (gameObject.name.Equals("Enemy 4 Toggle"))
            {

                Game.current.shooterSettings.enemy4B = temp;
            }
            else if (gameObject.name.Equals("Enemy 5 Toggle"))
            {

                Game.current.shooterSettings.enemy5B = temp;
            }

        }
        else if (GameObject.Find("Dropdown").GetComponent<Dropdown>().value == 2) {
            if (gameObject.name.Equals("Enemy 1 Toggle"))
            {

                Game.current.shooterSettings.enemy1S = temp;
            }
            else if (gameObject.name.Equals("Enemy 2 Toggle"))
            {
                Game.current.shooterSettings.enemy2S = temp;
            }
            else if (gameObject.name.Equals("Enemy 3 Toggle"))
            {

                Game.current.shooterSettings.enemy3S = temp;
            }
            else if (gameObject.name.Equals("Enemy 4 Toggle"))
            {

                Game.current.shooterSettings.enemy4S = temp;
            }
            else if (gameObject.name.Equals("Enemy 5 Toggle"))
            {

                Game.current.shooterSettings.enemy5S = temp;
            }


        }
        else if (GameObject.Find("Dropdown").GetComponent<Dropdown>().value == 3) {
            if (gameObject.name.Equals("Enemy 1 Toggle"))
            {

                Game.current.shooterSettings.enemy1G = temp;
            }
            else if (gameObject.name.Equals("Enemy 2 Toggle"))
            {

                Game.current.shooterSettings.enemy2G = temp;
            }
            else if (gameObject.name.Equals("Enemy 3 Toggle"))
            {

                Game.current.shooterSettings.enemy3G = temp;
            }
            else if (gameObject.name.Equals("Enemy 4 Toggle"))
            {

                Game.current.shooterSettings.enemy4G = temp;
            }
            else if (gameObject.name.Equals("Enemy 5 Toggle"))
            {

                Game.current.shooterSettings.enemy5G = temp;
            }

        }
        //When there needs to be at least one enemy selected per level
        if ((Game.current.shooterSettings.enemy1B + Game.current.shooterSettings.enemy2B + Game.current.shooterSettings.enemy3B + Game.current.shooterSettings.enemy4B + Game.current.shooterSettings.enemy5B) == 0)
        {
            Game.current.shooterSettings.enemy1B = 1;
            
            GameObject.Find("Enemy 1 Toggle").GetComponent<Toggle>().isOn = true;
            GameObject.Find("Error Panel").GetComponent<Image>().enabled = true;
            GameObject.Find("Error Panel").GetComponentInChildren<Text>().text = "ERROR: Must have at least one enemy selected.";

        }
        if ((Game.current.shooterSettings.enemy1S + Game.current.shooterSettings.enemy2S + Game.current.shooterSettings.enemy3S + Game.current.shooterSettings.enemy4S + Game.current.shooterSettings.enemy5S) == 0)
        {
            Game.current.shooterSettings.enemy1S = 1;
            
            GameObject.Find("Enemy 1 Toggle").GetComponent<Toggle>().isOn = true;
            GameObject.Find("Error Panel").GetComponent<Image>().enabled = true;
            GameObject.Find("Error Panel").GetComponentInChildren<Text>().text = "ERROR: Must have at least one enemy selected.";

        }
        if ((Game.current.shooterSettings.enemy1G + Game.current.shooterSettings.enemy2G + Game.current.shooterSettings.enemy3G + Game.current.shooterSettings.enemy4G + Game.current.shooterSettings.enemy5G) == 0)
        {
            Game.current.shooterSettings.enemy1G = 1;
            
            GameObject.Find("Enemy 1 Toggle").GetComponent<Toggle>().isOn = true;
            GameObject.Find("Error Panel").GetComponent<Image>().enabled = true;
            GameObject.Find("Error Panel").GetComponentInChildren<Text>().text = "ERROR: Must have at least one enemy selected.";
        }
    }
}
