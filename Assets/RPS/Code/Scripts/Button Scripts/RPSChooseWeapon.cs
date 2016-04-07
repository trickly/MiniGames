using UnityEngine;
using System.Collections;

public class RPSChooseWeapon : MonoBehaviour {


    private int weaponNum;
    //When mouse clicks on a weapon, selects that weapon for the user
    void OnMouseDown()
    {


        if (this.gameObject.tag == "Rock")
        {
            weaponNum = 1;
        }
        else if(this.gameObject.tag == "Paper")
        {
            weaponNum = 2;
        }
        else
        {
            weaponNum = 3;
        }
        GameObject.Find("Main CameraG").GetComponent<RPSGamePlay>().setUpGame(weaponNum); //leads into the setup of the game field
    }
}
