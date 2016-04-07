using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

//the most important class of the game, holding much of the mechanisms, actions, events, and variables in place
public class RPSGamePlay : MonoBehaviour
{

    private int myWeaponNum;
    private int enemyWeaponNum;
    private static int myWins;
    private static int totalGames;
    private static int enemyWins;

    void Awake() {
        DontDestroyOnLoad(this); //used to carry on values to the endstate screen to display results
    }
    void Start()
    {
        foreach (Canvas a in GameObject.Find("Main Menu").GetComponentsInChildren<Canvas>(true))
        {
            a.worldCamera = Camera.main;
            //a.planeDistance = 1;
        }

        myWins = 0;
        totalGames = 0;
        enemyWins = 0;
    }
    
    public void setWeapon(int weaponNum)
    {
        this.myWeaponNum = weaponNum;

    }
    public void enemyWeaponGenerator()
    {
        enemyWeaponNum = UnityEngine.Random.Range(1, 4); // randomly generates a number to select the opponent's weapon
    }

    public int getMyWeaponNum() {
        return myWeaponNum;
    }
    public int getEnemyWeaponNum() {
        return enemyWeaponNum;
    }
    public int getMyWins()
    {
        return myWins;
    }
    public int getEnemyWins()
    {
        return enemyWins;
    }
    public int getGamesPlayed()
    {
        return totalGames;
    }


    //Called every new round to set up the field
    public void setUpGame(int myWeaponNum)
    {
        showOrHideWeaponMenu(false); //hides the weapon menu
        setWeapon(myWeaponNum); //sets own weapon
        enemyWeaponGenerator(); //sets opponent's weapon
        showOrHideField(true); //shows the game field

       
        //Process wins/losses/total rounds

        if (myWeaponNum == enemyWeaponNum)
        {

        }
        else if ((myWeaponNum == 3 && enemyWeaponNum == 2) ||
             (myWeaponNum == 2 && enemyWeaponNum == 1) || (myWeaponNum == 1 && enemyWeaponNum == 3))
        {
            myWins++;
        }
        else
        {
            enemyWins++;
        }
        totalGames++;
        
        StartCoroutine(DelayNextRound()); // used to delay the result of the game
       
    }

    //Delays the display of the continue button 
    //for aesthetic purposes
    IEnumerator DelayNextRound()
    {
        yield return new WaitForSeconds(0.9f);
        GameObject.Find("Continue").GetComponent<Image>().enabled = true;
        GameObject.Find("ContinueText").GetComponent<Text>().enabled = true;
    }

    //Hides certain components (renderers) of the game objects for the weapon menu
    //Useful because there is no need to destroy them
    //One can simply hide or show them whenever convenient
    public void showOrHideWeaponMenu(bool show) {
        GameObject.Find("WeaponMenu").GetComponent<Canvas>().enabled = show;

        GameObject rockSelect = GameObject.Find("RockSelect");
        MeshRenderer[] childSRock = rockSelect.GetComponentsInChildren<MeshRenderer>();
        GameObject childBRock = GameObject.Find("Cube");
        childBRock.GetComponent<BoxCollider>().enabled = show;

        foreach (MeshRenderer m in childSRock)
        {
            m.enabled = show;
        }
        GameObject scissorsSelect = GameObject.Find("ScissorsSelect");
        MeshRenderer[] childSScissors = scissorsSelect.GetComponentsInChildren<MeshRenderer>();
        MeshCollider[] childBScissors = scissorsSelect.GetComponentsInChildren<MeshCollider>();
        foreach (MeshRenderer m in childSScissors)
        {
            m.enabled = show;
        }
        foreach (MeshCollider b in childBScissors)
        {
            b.enabled = show;
        }
        GameObject paperSelect = GameObject.Find("PaperSelect");
        paperSelect.GetComponent<MeshRenderer>().enabled = show;
        paperSelect.GetComponent<BoxCollider>().enabled = show;
    }

    //Hides certain components (renderers) of the game objects on the field
    //Useful because there is no need to destroy them
    //One can simply hide or show them whenever convenient

    public void showOrHideField (bool show)
    {

        GameObject.Find("Field").GetComponent<MeshRenderer>().enabled = show;
        GameObject.Find("Scoreboard").GetComponent<Canvas>().enabled = show;
        GameObject.Find("Continue").GetComponent<Image>().enabled = false; //keeps continue button hidden
        GameObject.Find("ContinueText").GetComponent<Text>().enabled = false; //keeps continue button hidden

        GameObject myWeapon = GameObject.Find("MyRock");
        GameObject enemyWeapon = GameObject.Find("MyRock");

        //based on weapon choice, will only show the selected weapon
        switch (myWeaponNum)
        {
            case 1:
                myWeapon = GameObject.Find("MyRock");
                MeshRenderer[] childRock = myWeapon.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer m in childRock)
                {
                    m.enabled = show;
                }
                break;
            case 3:
                myWeapon = GameObject.Find("MyScissors");
                MeshRenderer[] childScissors = myWeapon.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer m in childScissors)
                {
                    m.enabled = show;
                }
                break;
            case 2:
                myWeapon = GameObject.Find("MyPaper");
                myWeapon.GetComponent<MeshRenderer>().enabled = show;
                break;
        }

        switch (enemyWeaponNum)
        {
            case 1:
                enemyWeapon = GameObject.Find("EnemyRock");
                MeshRenderer[] childERock = enemyWeapon.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer m in childERock)
                {
                    m.enabled = show;
                }
                break;
            case 3:
                enemyWeapon = GameObject.Find("EnemyScissors");
                MeshRenderer[] childEScissors = enemyWeapon.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer m in childEScissors)
                {
                    m.enabled = show;
                }
                break;
            case 2:
                enemyWeapon = GameObject.Find("EnemyPaper");
                enemyWeapon.GetComponent<MeshRenderer>().enabled = show;
                break;
        }
    }

}
