using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Input_Number : MonoBehaviour {
    

    private int[] numOfEnemies;
    private string error;
    void Start()
    {
        var input = gameObject.GetComponent<InputField>();
        var se = new InputField.SubmitEvent();
        input.onEndEdit = se;
        se.AddListener(SubmitName);
        GameObject.Find("Error Panel").GetComponent<Image>().enabled = false;
        GameObject.Find("Error Panel").GetComponentInChildren<Text>().text = "";

    }

    private void SubmitName(string arg0)
    {
        int tempNumOfEnemies = 0;
        int x;
        Debug.Log(arg0);
        if (int.TryParse(arg0, out x))
        {//separated Parse statements bc first need to verify if string is parsable before checking if positive/0<=1000
            if (int.Parse(arg0) > 0 && int.Parse(arg0) <= 10000)
            {
                x = int.Parse(arg0);
                tempNumOfEnemies += int.Parse(GameObject.FindGameObjectWithTag("EnemySetting").GetComponent<InputField>().text);
                
                if (GameObject.Find("Dropdown").GetComponent<Dropdown>().value == 1)
                {
                    if ((tempNumOfEnemies < Game.current.shooterSettings.numEnemyS) && (tempNumOfEnemies < Game.current.shooterSettings.numEnemyG))
                    {
                        // GameObject.Find("Error Panel").GetComponent<Image>().enabled = false;
                        // GameObject.Find("Error Panel").GetComponentInChildren<Text>().text = "";
                        

                        if (gameObject.name.Equals("Enemy Num"))
                        {
                            Game.current.shooterSettings.numEnemyB = x;
                            gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.numEnemyB.ToString();
                        }
                        else if (gameObject.name.Equals("Enemy Num Max"))
                        {
                            if ((x < Game.current.shooterSettings.maxNumS) && (x < Game.current.shooterSettings.maxNumG))
                            {
                                Game.current.shooterSettings.maxNumB =x;
                                gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.maxNumB.ToString();

                            }
                            else
                            {
                                error = ("ERROR: Bronze level must have the lowest max score.");
                                returnPrevValue();
                            }
                        }
                    }
                    else
                    {
                        error = ("ERROR: Total number of enemies in Bronze level must be the least.");
                        returnPrevValue();

                    }
                }
                else if (GameObject.Find("Dropdown").GetComponent<Dropdown>().value == 2)
                {
                    if ((tempNumOfEnemies > Game.current.shooterSettings.numEnemyB) && (tempNumOfEnemies < Game.current.shooterSettings.numEnemyG))
                    {
                        if (gameObject.name.Equals("Enemy Num"))
                        {
                            Game.current.shooterSettings.numEnemyS = x;
                            gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.numEnemyS.ToString();
                            print(Game.current.shooterSettings.numEnemyS);

                        }
                        else if (gameObject.name.Equals("Enemy Num Max"))
                        {
                            if ((x > Game.current.shooterSettings.maxNumB) && (Game.current.shooterSettings.maxNumG > x))
                            {
                                Game.current.shooterSettings.maxNumS = x;
                                gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.maxNumS.ToString();
                                print(Game.current.shooterSettings.maxNumS);
                            }
                            else
                            {
                                error = ("ERROR: Silver level must have a max score in between Bronze and Gold.");
                                returnPrevValue();

                            }
                        }
                    }
                    else
                    {
                        error = ("ERROR: Total number of enemies in Silver level must be in between Bronze and Gold.");
                        returnPrevValue();


                    }
                }
                else if (GameObject.Find("Dropdown").GetComponent<Dropdown>().value == 3)
                {
                    if ((tempNumOfEnemies > Game.current.shooterSettings.numEnemyS) && (tempNumOfEnemies > Game.current.shooterSettings.numEnemyB))
                    {
                        if (gameObject.name.Equals("Enemy Num"))
                        {
                            Game.current.shooterSettings.numEnemyG = x;
                            gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.numEnemyG.ToString();
                            print(Game.current.shooterSettings.numEnemyG);

                        }
                        else if (gameObject.name.Equals("Enemy Num Max"))
                        {
                            if ((x > Game.current.shooterSettings.maxNumB) && (x > Game.current.shooterSettings.maxNumS))
                            {
                                Game.current.shooterSettings.maxNumG = x;
                                gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.maxNumG.ToString();
                                print(Game.current.shooterSettings.maxNumG);
                            }
                            else
                            {
                                error = ("ERROR: Gold Level must have the highest max score.");
                                returnPrevValue();
                            }
                        }
                    }
                    else
                    {
                        error = ("ERROR: Total number of enemies in Gold level must be the highest.");
                        returnPrevValue();
                    }
                }

            }
            else
            {
                isFalse();
                error = ("ERROR: Enter an integer between 0 - 1000.");
                returnPrevValue();
                // GameObject.Find("Error Panel").GetComponent<Image>().enabled = true;
                // GameObject.Find("Error Panel").GetComponentInChildren<Text>().text = "Error: Enter an integer between 0-1000";
            }
        }
        else
        {
            isFalse();
            error = ("ERROR: Enter an integer.");
            returnPrevValue();
             //  GameObject.Find("Error Panel").GetComponent<Image>().enabled = true;
            //GameObject.Find("Error Panel").GetComponentInChildren<Text>().text = "Error: Enter an integer";
        }
            GameObject.Find("Error Panel").GetComponent<Image>().enabled = true;
            GameObject.Find("Error Panel").GetComponentInChildren<Text>().text = error;
        error = "";
    }

    private void isFalse()
    {
    }

    private void returnPrevValue()
    {

        if (GameObject.Find("Dropdown").GetComponent<Dropdown>().value == 1)
        {

            if (gameObject.name.Equals("Enemy Num"))
            {
                gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.numEnemyS.ToString();
            }
            else if (gameObject.name.Equals("Enemy Num Max"))
            {
                gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.maxNumB.ToString();
            }
        }
        else if (GameObject.Find("Dropdown").GetComponent<Dropdown>().value == 2)
        {
            if (gameObject.name.Equals("Enemy Num"))
            {
                gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.numEnemyS.ToString();

            }
            else if (gameObject.name.Equals("Enemy Num Max"))
            {
                gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.maxNumS.ToString();

            }
        }
        else if (GameObject.Find("Dropdown").GetComponent<Dropdown>().value == 3)
        {
            if (gameObject.name.Equals("Enemy Num"))
            {
                gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.numEnemyG.ToString();

            }
            else if (gameObject.name.Equals("Enemy Num Max"))
            {

                gameObject.GetComponent<InputField>().text = Game.current.shooterSettings.maxNumG.ToString();
            }

        }
    }
}
