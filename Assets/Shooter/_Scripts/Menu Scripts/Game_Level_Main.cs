using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System;

public class Game_Level_Main : MonoBehaviour {
    
    Dropdown drop;
    
    // Use this for initialization
    void Start () {
        drop = gameObject.GetComponentInChildren<Dropdown>();
        drop.onValueChanged.AddListener(delegate {
            levelChange(drop);
        });

        //Maybe use OnEnable for this.
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemySetting"))
        {
            go.GetComponent<InputField>().enabled = false;
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemyActiveToggle"))
        {
            go.GetComponent<Toggle>().enabled = false;
            go.GetComponent<Toggle>().isOn = false;

        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemyShootToggle"))
        {
            go.GetComponent<Toggle>().enabled = false;
            go.GetComponentInChildren<Image>().enabled = false;
            go.GetComponent<Toggle>().isOn = false;
        }
        GameObject.Find("Shoot").GetComponent<Text>().enabled = false;

    }
    //Changes level based on the dropdown index
    //toggles and other values are updated based on the values
    private void levelChange(Dropdown target)
    {

        switch (target.value) {
            case 0://BRONZE
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemySetting"))
                {
                    go.GetComponent<InputField>().enabled = false;
                    go.GetComponent<InputField>().text = "";
                }
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemyActiveToggle")) {
                    go.GetComponent<Toggle>().isOn = false;
                    go.GetComponent<Toggle>().enabled = false;

                }
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemyShootToggle"))
                {
                    go.GetComponent<Toggle>().enabled = false;
                    go.GetComponentInChildren<Image>().enabled = false;

                    go.GetComponent<Toggle>().isOn = false;
                }
                GameObject.Find("Shoot").GetComponent<Text>().enabled = false;
                GameObject.Find("Enemy Num Max").GetComponent<InputField>().enabled = false;
                GameObject.Find("Enemy Num Max").GetComponent<InputField>().text = "";

                break;

            case 1://SILVER
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemySetting"))
                {
                    go.GetComponent<InputField>().enabled = true;
                }
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemyActiveToggle"))
                {
                    go.GetComponent<Toggle>().enabled = true;
                }
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemyShootToggle"))
                {
                    go.GetComponent<Toggle>().enabled = false;
                    go.GetComponent<Toggle>().isOn = false;
                    go.GetComponentInChildren<Image>().enabled = false;
                }

                GameObject.Find("Shoot").GetComponent<Text>().enabled = false;
                GameObject.Find("Enemy Num Max").GetComponent<InputField>().enabled = true;
                GameObject.Find("Enemy Num Max").GetComponent<InputField>().text = Game.current.shooterSettings.maxNumB.ToString();
                GameObject.Find("Enemy Num").GetComponent<InputField>().text = Game.current.shooterSettings.numEnemyB.ToString();

                setLevelVersion(GameObject.Find("Enemy 1 Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.enemy1B);
                setLevelVersion(GameObject.Find("Enemy 2 Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.enemy2B);
                setLevelVersion(GameObject.Find("Enemy 3 Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.enemy3B);
                setLevelVersion(GameObject.Find("Enemy 4 Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.enemy4B);
                setLevelVersion(GameObject.Find("Enemy 5 Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.enemy5B);
                break;

            case 2://GOLD
                //Shooting is allowed in this level

                foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemySetting"))
                {
                    go.GetComponent<InputField>().enabled = true;
                }
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemyActiveToggle"))
                {
                    go.GetComponent<Toggle>().enabled = true;
                }
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemyShootToggle"))
                {
                    go.GetComponent<Toggle>().enabled = false;
                    go.GetComponent<Toggle>().isOn = false;
                    go.GetComponentInChildren<Image>().enabled = false;
                }
                GameObject.Find("Shoot").GetComponent<Text>().enabled = false;
                GameObject.Find("Enemy Num Max").GetComponent<InputField>().enabled = true;
                GameObject.Find("Enemy Num Max").GetComponent<InputField>().text = Game.current.shooterSettings.maxNumS.ToString();

                GameObject.Find("Enemy Num").GetComponent<InputField>().text = Game.current.shooterSettings.numEnemyS.ToString();

                setLevelVersion(GameObject.Find("Enemy 1 Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.enemy1S);
                setLevelVersion(GameObject.Find("Enemy 2 Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.enemy2S);
                setLevelVersion(GameObject.Find("Enemy 3 Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.enemy3S);
                setLevelVersion(GameObject.Find("Enemy 4 Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.enemy4S);
                setLevelVersion(GameObject.Find("Enemy 5 Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.enemy5S);


                break;
            case 3:

                foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemySetting"))
                {
                    go.GetComponent<InputField>().enabled = true;
                }
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemyActiveToggle"))
                {
                    go.GetComponent<Toggle>().enabled = true;
                }
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemyShootToggle"))
                {
                    go.GetComponent<Toggle>().enabled = true;
                    go.GetComponentInChildren<Image>().enabled = true;
                }
                GameObject.Find("Bar").GetComponent<Image>().enabled = false;

                GameObject.Find("Shoot").GetComponent<Text>().enabled = true;
                GameObject.Find("Enemy Num Max").GetComponent<InputField>().enabled = true;
                GameObject.Find("Enemy Num Max").GetComponent<InputField>().text = Game.current.shooterSettings.maxNumG.ToString();

                GameObject.Find("Enemy Num").GetComponent<InputField>().text = Game.current.shooterSettings.numEnemyG.ToString();


                setLevelVersion(GameObject.Find("Enemy 1 Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.enemy1G);
                setLevelVersion(GameObject.Find("Enemy 2 Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.enemy2G);
                setLevelVersion(GameObject.Find("Enemy 3 Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.enemy3G);
                setLevelVersion(GameObject.Find("Enemy 4 Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.enemy4G);
                setLevelVersion(GameObject.Find("Enemy 5 Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.enemy5G);

                setLevelVersion(GameObject.Find("Enemy 1 Shoot Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.shootableEnemy1);
                setLevelVersion(GameObject.Find("Enemy 2 Shoot Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.shootableEnemy2);
                setLevelVersion(GameObject.Find("Enemy 3 Shoot Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.shootableEnemy3);
                setLevelVersion(GameObject.Find("Enemy 4 Shoot Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.shootableEnemy4);
                setLevelVersion(GameObject.Find("Enemy 5 Shoot Toggle").GetComponent<Toggle>(), Game.current.shooterSettings.shootableEnemy5);
                break;
        }
    }
    //Gets the dropdown index/ level
    public int getLevel() {
        return drop.value;
    }
    //Used to manually set the value of the index
    public void SetDropdownIndex(int index)
    {
        drop.value = index;
    }
    //Based on the provided toggle and int, will turn off toggle
    private void setLevelVersion(Toggle tog, int playerPref) {
        if (playerPref == 1)
        {
            tog.isOn = true;
        }
        else
        {
            tog.isOn = false;
        }
    }
    public void toggleOff() {
        if (drop != null) {
            drop.value = 0;
        }
    }
    
}
