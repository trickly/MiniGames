using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour {
    public GameObject gLC;
    public GameObject mMC;
    public GameObject configurationsC;
    public GameObject historyC;
    public GameObject startC;

    public GameObject enemiesC;
    public GameObject audioC;
    public GameObject backgroundC;
    public Sprite[] backgrounds;


    public static GameObject mainMenu;
	// Use this for initialization
	void Start () {

        mainMenu = this.gameObject;
        //Sets up the camera to view the menu from the previous scene
        foreach (Canvas a in GameObject.Find("Main Menu").GetComponentsInChildren<Canvas>(true)) {
            a.worldCamera = Camera.main;
        }
        PlayerPrefsSetUp();
        setBackground();
        GameObject.Find("Background Music Source").GetComponent<AudioSource>().Play();

    }

    void RecordGameLog() {
        Game.current.shooterHistory.Add(new GameLog());
        Game.current.shooterHistory[Game.current.shooterHistory.Count-1].startTime = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
    }
    // Update is called once per frame


    private void setBackground()
    {
        gLC.GetComponent<Image>().overrideSprite = backgrounds[Game.current.shooterSettings.background];
        mMC.GetComponent<Image>().overrideSprite = backgrounds[Game.current.shooterSettings.background];
        configurationsC.GetComponent<Image>().overrideSprite = backgrounds[Game.current.shooterSettings.background];
        historyC.GetComponent<Image>().overrideSprite = backgrounds[Game.current.shooterSettings.background];
        startC.GetComponent<Image>().overrideSprite = backgrounds[Game.current.shooterSettings.background];
        enemiesC.GetComponent<Image>().overrideSprite = backgrounds[Game.current.shooterSettings.background];
        audioC.GetComponent<Image>().overrideSprite = backgrounds[Game.current.shooterSettings.background];
        backgroundC.GetComponent<Image>().overrideSprite = backgrounds[Game.current.shooterSettings.background];
    }
    private void PlayerPrefsSetUp()
    {
        PlayerPrefs.SetInt("Number of Enemy B", Game.current.shooterSettings.numEnemyB);
        PlayerPrefs.SetInt("Number of Enemy S", Game.current.shooterSettings.numEnemyS);
        PlayerPrefs.SetInt("Number of Enemy G", Game.current.shooterSettings.numEnemyG);
        PlayerPrefs.SetInt("Max Num B", Game.current.shooterSettings.maxNumB);
        PlayerPrefs.SetInt("Max Num S", Game.current.shooterSettings.maxNumS);
        PlayerPrefs.SetInt("Max Num G", Game.current.shooterSettings.maxNumG);

        PlayerPrefs.SetInt("Enemy 1B", Game.current.shooterSettings.enemy1B);
        PlayerPrefs.SetInt("Enemy 2B", Game.current.shooterSettings.enemy2B);
        PlayerPrefs.SetInt("Enemy 3B", Game.current.shooterSettings.enemy3B);
        PlayerPrefs.SetInt("Enemy 4B", Game.current.shooterSettings.enemy4B);
        PlayerPrefs.SetInt("Enemy 5B", Game.current.shooterSettings.enemy5B);
        PlayerPrefs.SetInt("Enemy 1S", Game.current.shooterSettings.enemy1S);
        PlayerPrefs.SetInt("Enemy 2S", Game.current.shooterSettings.enemy2S);
        PlayerPrefs.SetInt("Enemy 3S", Game.current.shooterSettings.enemy3S);
        PlayerPrefs.SetInt("Enemy 4S", Game.current.shooterSettings.enemy4S);
        PlayerPrefs.SetInt("Enemy 5S", Game.current.shooterSettings.enemy5S);
        PlayerPrefs.SetInt("Enemy 1G", Game.current.shooterSettings.enemy1G);
        PlayerPrefs.SetInt("Enemy 2G", Game.current.shooterSettings.enemy2G);
        PlayerPrefs.SetInt("Enemy 3G", Game.current.shooterSettings.enemy3G);
        PlayerPrefs.SetInt("Enemy 4G", Game.current.shooterSettings.enemy4G);
        PlayerPrefs.SetInt("Enemy 5G", Game.current.shooterSettings.enemy5G);

        PlayerPrefs.SetInt("Enemy 1Shoot", Game.current.shooterSettings.shootableEnemy1);
        PlayerPrefs.SetInt("Enemy 2Shoot", Game.current.shooterSettings.shootableEnemy2);
        PlayerPrefs.SetInt("Enemy 3Shoot", Game.current.shooterSettings.shootableEnemy3);
        PlayerPrefs.SetInt("Enemy 4Shoot", Game.current.shooterSettings.shootableEnemy4);
        PlayerPrefs.SetInt("Enemy 5Shoot", Game.current.shooterSettings.shootableEnemy5);
        PlayerPrefs.SetString("Enemy 1Color", Game.current.shooterSettings.colourEnemy1);
        PlayerPrefs.SetString("Enemy 2Color", Game.current.shooterSettings.colourEnemy2);
        PlayerPrefs.SetString("Enemy 3Color", Game.current.shooterSettings.colourEnemy3);
        PlayerPrefs.SetString("Enemy 4Color", Game.current.shooterSettings.colourEnemy4);
        PlayerPrefs.SetString("Enemy 5Color", Game.current.shooterSettings.colourEnemy5);

        PlayerPrefs.SetInt("Enemy 1Points", Game.current.shooterSettings.pointsEnemy1);
        PlayerPrefs.SetInt("Enemy 2Points", Game.current.shooterSettings.pointsEnemy2);
        PlayerPrefs.SetInt("Enemy 3Points", Game.current.shooterSettings.pointsEnemy3);
        PlayerPrefs.SetInt("Enemy 4Points", Game.current.shooterSettings.pointsEnemy4);
        PlayerPrefs.SetInt("Enemy 5Points", Game.current.shooterSettings.pointsEnemy5);

        PlayerPrefs.SetInt("Background B", Game.current.shooterSettings.backgroundB);
        PlayerPrefs.SetInt("Background S", Game.current.shooterSettings.backgroundS);
        PlayerPrefs.SetInt("Background G", Game.current.shooterSettings.backgroundG);
        PlayerPrefs.SetInt("Background", Game.current.shooterSettings.background);
    }
    public void transitionSound() {
        GameObject.Find("Game Sounds Source").GetComponent<AudioSource>().Play();
    }

    public void ReturnHome() {
        SceneManager.LoadScene("_Main_Start");
    }

    // Obsolete code from stage 2
    public GameObject getMMC()
    {
        return mMC;
    }
    
    //Start Menu movements
    public void GoToGame() {
        SceneManager.LoadScene("_Shooter_Game");
    }
    public void GoToMainMenu() {
        mMC.SetActive(true);
        startC.SetActive(false);
    }
    public void ExitGame() {
        //Destroy(GameObject.Find("Background Music Source"));
        // Destroy(GameObject.Find("Winning Music Source"));
        //Destroy(GameObject.Find("Game Sounds Source"));
        GameObject.Find("Background Music Source").GetComponent<AudioSource>().Stop();

        GameObject.Find("Background Music").GetComponent<AudioSource>().Play();
        GameObject.Find("Main Menu").GetComponent<MainMenu>().TurnMenuBar(true);


        SceneManager.LoadScene("_Main_Start");

    }


    //Main menu movements
    public void GoToGameLevels() {
        gLC.SetActive(true);
        mMC.SetActive(false);
    }

    public void GoToHistory()
    {
        mMC.SetActive(false);
        historyC.SetActive(true);
    }
    public void GoToConfigurations()
    {
        mMC.SetActive(false);
        configurationsC.SetActive(true);
    }

    //game level menu movements
    public void BackToMainFromGame()
    {
        mMC.SetActive(true);
        gLC.SetActive(false);

    }

    //Configurations menu movements
    public void BackToMainFromConfigurations()
    {
        mMC.SetActive(true);
        configurationsC.SetActive(false);
    }
    public void BackToConfigrationsFromEnemies()
    {
        configurationsC.SetActive(true);
        enemiesC.SetActive(false);
    }
    public void BackToConfigrationsFromAudio()
    {
        configurationsC.SetActive(true);
        audioC.SetActive(false);
    }
    public void BackToConfigrationsFromBackground()
    {
        configurationsC.SetActive(true);
        backgroundC.SetActive(false);
        setBackground();
    }
    public void GoToEnemies()
    {
        configurationsC.SetActive(false);
        enemiesC.SetActive(true);
    }
    public void GoToAudio()
    {
        configurationsC.SetActive(false);
        audioC.SetActive(true);
        GameObject.Find("Background Music Source").GetComponent<AudioSource>().Pause();
    }
    public void GoToBackground()
    {
        configurationsC.SetActive(false);
        backgroundC.SetActive(true);
    }
    
    //History Menu movements
    //HAVENT PUT IN OBJECT IN INSPECTOR
    public void BackToMainFromHistory()
    {
        mMC.SetActive(true);
        historyC.SetActive(false);

    }

    public void GoToStartScreen()
    {
        mMC.SetActive(false);
        startC.SetActive(true);
    }
}
