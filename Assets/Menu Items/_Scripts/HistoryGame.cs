using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//Associated with the history of each game 
//displays ALL users with history related to the game. like a high score board of sorts
public class HistoryGame : MonoBehaviour
{

    public Button userInfoPrefab;
    public GameObject grid;
    public Text text;
    public string name;
    //Since not much interaction on the history game screen, much of the code is based on when the screen turns on/off
    void OnEnable()
    {
        List<GameLog> gameHistory = new List<GameLog>(); //temporary list to contain and display whatever game's history

        text.text = "View the history of all the playthroughs for this game.";
        for (int i = SaveLoad.savedGames.Count - 1; i >= 0; i--)
        {
            //Keys Used to make this script more object oriented with unity. 
            //multiple instances of this script instead of multiple versions for diff game canvases
            if (name == "SS")
            {
                gameHistory = SaveLoad.savedGames[i].shooterHistory;
            }
            else if (name == "AP")
            {
                gameHistory = SaveLoad.savedGames[i].appleHistory;

            }
            else if (name == "RPS")
            {
                gameHistory = SaveLoad.savedGames[i].rpsHistory;

            }
            else if (name == "MEM")
            {
                gameHistory = SaveLoad.savedGames[i].memoryHistory;

            }

            //Instantiates prefab of userinfo 
            for (int x = gameHistory.Count - 1; x >= 0; x--)
            {
                Button loginInfo = (Button)Instantiate(userInfoPrefab);
                string userInfo = "Username: " + SaveLoad.savedGames[i].username;
                userInfo += "\nDate/Time: " + gameHistory[x].startTime + " \nPlayed for: ";
                if (gameHistory[x].secondsPlayed >= 3600)
                {
                    userInfo += ((int)(gameHistory[x].secondsPlayed / 3600)).ToString() + " hours ";

                }
                //Simple modulus and division to set string into readable format
                userInfo += ((int)(gameHistory[x].secondsPlayed % 3600 / 60)).ToString() + " min "
                    + (((int)(gameHistory[x].secondsPlayed % 60 / 10)).ToString()
                    + (((float)gameHistory[x].secondsPlayed % 60) % 10).ToString()) + " sec";



                userInfo += "\nScore: " +gameHistory[x].score;
                //For Space Shooter only, the highest level attained is shown
                if (name == "SS")
                {
                    userInfo += "\nHighest Level Achieved: ";
                    switch (gameHistory[x].level)
                    {
                        case (0):
                            userInfo += "Bronze";
                            break;
                        case (1):
                            userInfo += "Silver";
                            break;
                        case (2):
                            userInfo += "Gold";
                            break;
                    }
                }
                loginInfo.GetComponentInChildren<Text>().text = userInfo;
                loginInfo.transform.SetParent(grid.transform, false); //Fills up the scrollable grid with these layout elements of user information
            }
        }

    }

    //Destroys the gameobjects when exiting. Needed to clear and refill for future use
    void OnDisable() {
        foreach (Button go in grid.GetComponentsInChildren<Button>()) {
            Destroy(go.gameObject); 
        }
    }


}
