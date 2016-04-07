using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shoot_Enemy : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var tog = gameObject.GetComponent<Toggle>();
        var se = new Toggle.ToggleEvent();
        tog.onValueChanged = se;
        se.AddListener(ifActive);
    }


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
       if (GameObject.Find("Dropdown").GetComponent<Dropdown>().value == 3)
        {
            if (gameObject.name.Equals("Enemy 1 Shoot Toggle"))
            {
                Game.current.shooterSettings.shootableEnemy1 = temp;
            }
            else if (gameObject.name.Equals("Enemy 2 Shoot Toggle"))
            {
                Game.current.shooterSettings.shootableEnemy2 = temp;
            }
            else if (gameObject.name.Equals("Enemy 3 Shoot Toggle"))
            {
                Game.current.shooterSettings.shootableEnemy3 = temp;

            }
            else if (gameObject.name.Equals("Enemy 4 Shoot Toggle"))
            {
                Game.current.shooterSettings.shootableEnemy4 = temp;
            }
            else if (gameObject.name.Equals("Enemy 5 Shoot Toggle"))
            {

                Game.current.shooterSettings.shootableEnemy5 = temp;
            }

        }
       //Sets the first enemy to shootable as at least one enemy must shoot in the gold level
        if ((Game.current.shooterSettings.shootableEnemy1 + Game.current.shooterSettings.shootableEnemy2 + Game.current.shooterSettings.shootableEnemy3 + Game.current.shooterSettings.shootableEnemy4 + Game.current.shooterSettings.shootableEnemy5) == 0)
        {
            Game.current.shooterSettings.shootableEnemy1 = 1;
            GameObject.Find("Enemy 1 Shoot Toggle").GetComponent<Toggle>().isOn = true;
            GameObject.Find("Error Panel").GetComponent<Image>().enabled = true;
            GameObject.Find("Error Panel").GetComponentInChildren<Text>().text = "ERROR: Must have at least one enemy that can shoot.";

        }
    }
}
