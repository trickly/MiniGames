using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Associated with the history of the entire game 
//displays ALL users if admin. 
//display only one's own if not

public class HistoryMain : MonoBehaviour
{

    public Button userInfoPrefab;
    public GameObject grid;
    public Text text;
    


    void OnEnable()
    {
        if (Game.current.admin)
        {
            //if admin, displays list of each user account's login history
            text.text = "View the history of all the log-ins in the system.";
            for (int i = SaveLoad.savedGames.Count - 1; i >= 0; i--)
            {
                //Instantiates prefab of userinfo 
                for (int x = SaveLoad.savedGames[i].loginHistory.Count - 1; x >= 0; x--)
                {
                    Button loginInfo = (Button)Instantiate(userInfoPrefab);
                    string userInfo = "Username: " + SaveLoad.savedGames[i].username;
                    userInfo += "\nStatus: ";
                    if (SaveLoad.savedGames[i].admin)
                    {
                        userInfo += "ADMIN";
                    }
                    else if (SaveLoad.savedGames[i].blocked)
                    {
                        userInfo += "BLOCKED";
                    }
                    else if (SaveLoad.savedGames[i].firstLogin)
                    {
                        userInfo += "NEW";
                    }
                    else
                    {
                        userInfo += "NORMAL";
                    }
                    userInfo += "\nLog-in Date/Time: " + SaveLoad.savedGames[i].loginHistory[x].logInTime
                                         + "\n Played For: ";
                    //Modulus and division is used to change seconds into presentable data
                    if (SaveLoad.savedGames[i].loginHistory[x].secondsPlayed >= 3600)
                    {
                        userInfo += ((int)(SaveLoad.savedGames[i].loginHistory[x].secondsPlayed / 3600)).ToString() + " hours ";

                    }
                    userInfo += ((int)(SaveLoad.savedGames[i].loginHistory[x].secondsPlayed % 3600 / 60)).ToString() + " min "
                        + (((int)(SaveLoad.savedGames[i].loginHistory[x].secondsPlayed % 60 / 10)).ToString()
                        + (((float)SaveLoad.savedGames[i].loginHistory[x].secondsPlayed % 60) % 10).ToString()) + " sec";

                    //SaveLoad.savedGames[i].loginHistory[x].secondsPlayed + "s";
                    loginInfo.GetComponentInChildren<Text>().text = userInfo;
                    loginInfo.transform.SetParent(grid.transform, false);
                    //   loginInfo.transform.parent = grid.transform;
                    //  loginInfo.transform.localScale = new Vector3(1, 1, 1);

                }
            }

        }
        else
        {
            //if not admin, show only one's own log in history
            text.text = "View the history of all your log-ins.";
            //Instantiates prefab of userinfo 
            for (int i = Game.current.loginHistory.Count - 1; i >= 0; i--)
            {
                Button loginInfo = (Button)Instantiate(userInfoPrefab);
                string userInfo = "Log-in Date/Time: " + Game.current.loginHistory[i].logInTime
                                + "\nPlayed For: ";
                //Modulus and division is used to change seconds into presentable data
                if (Game.current.loginHistory[i].secondsPlayed >= 3600)
                {
                    userInfo += ((int)(Game.current.loginHistory[i].secondsPlayed / 3600)).ToString() + " hours ";

                }

                userInfo += ((int)(Game.current.loginHistory[i].secondsPlayed % 3600 / 60)).ToString() + " min "
                    + (((int)Game.current.loginHistory[i].secondsPlayed % 60 / 10)).ToString()
                    + (((float)Game.current.loginHistory[i].secondsPlayed % 60) % 10).ToString() + " sec";
                loginInfo.GetComponentInChildren<Text>().text = userInfo;
                loginInfo.transform.SetParent(grid.transform, false);

            }
        }

    }


    //Destroys the gameobjects when exiting. Needed to clear and refill for future use
    void OnDisable()
    {
        foreach (Button go in grid.GetComponentsInChildren<Button>())
        {
            Destroy(go.gameObject);
        }
    }
}
