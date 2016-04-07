using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemies_Score : MonoBehaviour
{

    private bool[] enemyPoints;
    private string error;
    void Start()
    {

        enemyPoints = new bool[5];
        for (int i = 0; i < enemyPoints.Length; i++)
        {
            enemyPoints[i] = true;
        }

        //Gets input from user
        var input = gameObject.GetComponent<InputField>();
        var se = new InputField.SubmitEvent();
        input.onEndEdit = se;
        se.AddListener(SubmitName);

    }
    //Recieves the value in the input field and sorts it accordingly
    //Shows error messages when needed
    private void SubmitName(string arg0)
    {
        int tempNumOfEnemies = 0;
        int x;
        Debug.Log(arg0);
        //Created my own parse functions even though unity has this function built in..
        if (int.TryParse(arg0, out x))
        {//separated Parse statements bc first need to verify if string is parsable before checking if positive/0<=1000
            if (int.Parse(arg0) > 0 && int.Parse(arg0) <= 1000)
            {
                x = int.Parse(arg0);

                foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemySetting"))
                {

                    tempNumOfEnemies += int.Parse(go.GetComponent<InputField>().text);
                }


                if (gameObject.name.Equals("Enemy 1 Points"))
                {
                    enemyPoints[0] = true;
                    Game.current.shooterSettings.pointsEnemy1 = x;
                    gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.pointsEnemy1.ToString();

                }
                else if (gameObject.name.Equals("Enemy 2 Points"))
                {
                    enemyPoints[1] = true;
                    Game.current.shooterSettings.pointsEnemy2 = x;
                    gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.pointsEnemy2.ToString();

                }
                else if (gameObject.name.Equals("Enemy 3 Points"))
                {
                    enemyPoints[2] = true;
                    Game.current.shooterSettings.pointsEnemy3 = x;
                    gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.pointsEnemy3.ToString();

                }
                else if (gameObject.name.Equals("Enemy 4 Points"))
                {
                    enemyPoints[3] = true;
                    Game.current.shooterSettings.pointsEnemy4 = x;
                    gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.pointsEnemy4.ToString();

                }
                else if (gameObject.name.Equals("Enemy 5 Points"))
                {
                    enemyPoints[4] = true;
                    Game.current.shooterSettings.pointsEnemy5 = x;
                    gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.pointsEnemy5.ToString();
                }
                error = "";
            }
            else
            {
                isFalse();
                error = ("ERROR: Enter an integer between 0 - 1000.");
                returnPrevValue();

            }
        }
        else
        {
            isFalse();
            error = ("ERROR: Enter an integer.");
            returnPrevValue();
        }
        GameObject.Find("Error").GetComponentInChildren<Text>().text = error;
    }

    private void isFalse()
    {
    }

    //function returns the previous value of the input field's text when something entered is rejected
    private void returnPrevValue()
    {

        if (gameObject.name.Equals("Enemy 1 Points"))
        {
            gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.pointsEnemy1.ToString();
        }
        else if (gameObject.name.Equals("Enemy 2 Points"))
        {
            gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.pointsEnemy2.ToString();

        }
        else if (gameObject.name.Equals("Enemy 3 Points"))
        {
            gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.pointsEnemy3.ToString();

        }
        else if (gameObject.name.Equals("Enemy 4 Points"))
        {
            gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.pointsEnemy4.ToString();

        }
        else if (gameObject.name.Equals("Enemy 5 Points"))
        {
            gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.pointsEnemy5.ToString();

        }
    }
}
