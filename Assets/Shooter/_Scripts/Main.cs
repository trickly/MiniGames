using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    static public Main S;
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 2f; // # Enemies/second
    public float enemySpawnPadding = 1.5f; // Padding for position
    public WeaponDefinition[] weaponDefinitions;
    public GameObject prefabPowerUp;
    public WeaponType[] powerUpFrequency = new WeaponType[] {
                                    WeaponType.blaster, WeaponType.blaster,
                                    WeaponType.spread,
                                    WeaponType.shield };
    public bool ________________;
    public WeaponType[] activeWeaponTypes;
    public float enemySpawnRate; // Delay between Enemy spawns
    static public Dictionary<WeaponType, WeaponDefinition> W_DEFS;
    public Sprite[] backgrounds;
    static public int score;
    static public int[] numOfEnemiesKilled;
    public AudioClip laser;
    public AudioClip explosion;
    public AudioSource[] gameSounds;
    public AudioSource winningSound;
    //public AudioSource backgroundSound;
    private bool setUp;
    private bool silver;
    public bool gold;
    public Material[] enemyMat;
    public GameObject pauseCanvas;
    public int maxNumber;
    public bool[] prefabEnemiesDisabled;

    void Awake()
    {

        S = this;
        foreach (Canvas a in GameObject.Find("Main Menu").GetComponentsInChildren<Canvas>(true))
        {
            a.worldCamera = Camera.main;
        }

        W_DEFS = new Dictionary<WeaponType, WeaponDefinition>();
        Utils.SetCameraBounds(this.GetComponent<Camera>());
        foreach (WeaponDefinition def in weaponDefinitions)
        {
            W_DEFS[def.type] = def;
        }
        winningSound = GameObject.Find("Winning Music Source").GetComponent<AudioSource>();
        gameSounds = GameObject.Find("Game Sounds Source").GetComponents<AudioSource>();
        //backgroundSound = GameObject.Find("Background Music Source").GetComponent<AudioSource>();
    }
    void Start()
    {

        // A generic Dictionary with WeaponType as the key
        setUp = true;
        enemySpawnPerSecond = 0.8f;
        enemySpawnRate = 1f / enemySpawnPerSecond;

        SpawnEnemy();

        prefabEnemiesDisabled = new bool[5];
        Utils.SetCameraBounds(this.GetComponent<Camera>());
        activeWeaponTypes = new WeaponType[weaponDefinitions.Length];
        for (int i = 0; i < weaponDefinitions.Length; i++)
        {
            activeWeaponTypes[i] = weaponDefinitions[i].type;
        }
        score = 0;
        numOfEnemiesKilled = new int[5];
        maxNumber = PlayerPrefs.GetInt("Max Num B");
        CheckScore();
        SetUpEnemies();
        UpdateScreen();
        gameSounds = GameObject.Find("Game Sounds Source").GetComponents<AudioSource>();
        winningSound = GameObject.Find("Winning Music Source").GetComponent<AudioSource>();
        silver = true;
        gold = true;
        pauseCanvas.SetActive(false);
        RecordGameLog1();

    }
    static public WeaponDefinition GetWeaponDefinition(WeaponType wt)
    {
        // Check to make sure that the key exists in the Dictionary
        // Attempting to retrieve a key that didn't exist, would throw an error,
        // so the following if statement is important.
 
        {
            return (W_DEFS[wt]);
        }
        // This will return a definition for WeaponType.none,
        // which means it has failed to find the WeaponDefinition
        return (new WeaponDefinition());
    }
    public void SpawnEnemy()
    {
        // Pick a random Enemy prefab to instantiate[
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxNumber)
        {
            int ndx = Random.Range(0, prefabEnemies.Length);

            SetColour();
        
            if (prefabEnemiesDisabled[ndx])
            {
                for (int i = 0; i < prefabEnemiesDisabled.Length; i++)
                {
                    if (!prefabEnemiesDisabled[i])
                    {
                        ndx = i;
                    }
                }
            }
            
            GameObject go = Instantiate(prefabEnemies[ndx]) as GameObject;
            // Position the Enemy above the screen with a random x position
            Vector3 pos = Vector3.zero;
            float xMin = Utils.camBounds.min.x + enemySpawnPadding;
            float xMax = Utils.camBounds.max.x - enemySpawnPadding;
            pos.x = Random.Range(xMin, xMax);
            pos.y = Utils.camBounds.max.y + enemySpawnPadding;
            go.transform.position = pos;
          }
        Invoke("SpawnEnemy", enemySpawnRate); // 3
    }
    //Start of game log at the moment the user enters the game screen
    void RecordGameLog1()
    {
        Game.current.shooterHistory.Add(new GameLog());
        Game.current.shooterHistory[Game.current.shooterHistory.Count - 1].startTime = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        print(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        print(Game.current.shooterHistory[Game.current.shooterHistory.Count - 1].startTime);
    }
    //Add onto the Game log of the current playthrough. 
    //Activated whenever the user goes back to the main menu
    //Menu-based restart will count as another playthrough
    public void RecordGameLog2() {
        //score achieved
        Game.current.shooterHistory[Game.current.shooterHistory.Count - 1].score = score;
        //Highest level achieved
        if (!gold)
        {
            Game.current.shooterHistory[Game.current.shooterHistory.Count - 1].level = 2;
        }
        else if (!silver)
        {
            Game.current.shooterHistory[Game.current.shooterHistory.Count - 1].level = 1;
        }
        else {
            Game.current.shooterHistory[Game.current.shooterHistory.Count - 1].level = 0;
        }
        //Finds the number of seconds played by subtracting the end game time with the login time and converting it into seconds
        //login time is also parsed!
        Game.current.shooterHistory[Game.current.shooterHistory.Count - 1].secondsPlayed = (int)(float)System.DateTime.Now.Subtract(System.DateTime.Parse(Game.current.shooterHistory[Game.current.shooterHistory.Count - 1].startTime)).TotalSeconds;
    }
    //Delays the restart by the given float
    public void DelayedRestart(float delay)
    {
        Invoke("MainMenu", delay);
    }
    //Restarts tehe game from the 
    public void Restart()
    {
        //Should a restart count as part of the game log?
        //prob not
        GameObject.Find("Background Music Source").GetComponent<AudioSource>().UnPause();

        Time.timeScale = 1;
        SceneManager.LoadScene("_Shooter_Game");
    }
    public void MainMenu()
    {
        RecordGameLog2();
        Time.timeScale = 1;
        SceneManager.LoadScene("_Shooter_Start");

    }
    public void PauseGame()
    {
        pauseCanvas.SetActive(true);
        GameObject.Find("Background Music Source").GetComponent<AudioSource>().Pause();

        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        GameObject.Find("Background Music Source").GetComponent<AudioSource>().UnPause();

        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }
    public void TransitionSound()
    {
        GameObject.Find("Game Sounds Source").GetComponent<AudioSource>().Play();
    }
    public void ShipDestroyed(Enemy e)
    {
        gameSounds[2].Play();

        // Potentially generate a PowerUp
        if (Random.value <= e.powerUpDropChance)
        {
            // Random.value generates a value between 0 & 1 (though never == 1)
            // If the e.powerUpDropChance is 0.50f, a PowerUp will be generated
            // 50% of the time. For testing, it's now set to 1f.
            // Choose which PowerUp to pick
            // Pick one from the possibilities in powerUpFrequency
            int ndx = Random.Range(0, powerUpFrequency.Length);
            WeaponType puType = powerUpFrequency[ndx];
            // Spawn a PowerUp
            GameObject go = Instantiate(prefabPowerUp) as GameObject;
            PowerUp pu = go.GetComponent<PowerUp>();
            // Set it to the proper WeaponType
            pu.SetType(puType);
            // Set it to the position of the destroyed ship
            pu.transform.position = e.transform.position;
        }
        for (int i = 0; i < 5; i++)
        {
            if (e.typeEnemy == i)
            {
                ChangeScore(i);
                CheckScore();
                UpdateNumOfEnemies(i);

            }
        }


    }
    private void ChangeScore(int enemyType)
    {
        switch (enemyType)
        {
            case 0:
                score += PlayerPrefs.GetInt("Enemy 1Points");
                break;
            case 1:
                score += PlayerPrefs.GetInt("Enemy 2Points");
                break;
            case 2:
                score += PlayerPrefs.GetInt("Enemy 3Points");
                break;
            case 3:
                score += PlayerPrefs.GetInt("Enemy 4Points");
                break;
            case 4:
                score += PlayerPrefs.GetInt("Enemy 5Points");
                break;

        }
    }
    private void CheckScore()
    {
        SumUpEnemiesKilled();
        GameObject.Find("Score").GetComponent<Text>().text = "Score: " + score.ToString();
        if (SumUpEnemiesKilled() > PlayerPrefs.GetInt("Number of Enemy S") && gold)
        {
            winningSound.Play();
            GameObject.Find("Game Level").GetComponent<Text>().text = "Gold";
            GameObject.Find("Background Canvas").GetComponentInChildren<Image>().overrideSprite = backgrounds[PlayerPrefs.GetInt("Background G")];
            maxNumber = PlayerPrefs.GetInt("Max Num G");
            if (PlayerPrefs.GetInt("Enemy 1G") == 0)
            {
                prefabEnemiesDisabled[0] = true;
            }
            else { prefabEnemiesDisabled[0] = false; }
            if (PlayerPrefs.GetInt("Enemy 2G") == 0)
            {
                prefabEnemiesDisabled[1] = true;

            }
            else { prefabEnemiesDisabled[1] = false; }
            if (PlayerPrefs.GetInt("Enemy 3G") == 0)
            {
                prefabEnemiesDisabled[2] = true;

            }
            else { prefabEnemiesDisabled[2] = false; }
            if (PlayerPrefs.GetInt("Enemy 4G") == 0)
            {
                prefabEnemiesDisabled[3] = true;

            }
            else { prefabEnemiesDisabled[3] = false; }
            if (PlayerPrefs.GetInt("Enemy 5G") == 0)
            {
                prefabEnemiesDisabled[4] = true;

            }
            else { prefabEnemiesDisabled[4] = false; }
            enemySpawnPerSecond = 5f;
            gold = false;
        }
        else if (SumUpEnemiesKilled() > PlayerPrefs.GetInt("Number of Enemy B") && silver)
        {
            GameObject.Find("Game Level").GetComponent<Text>().text = "Silver";
            GameObject.Find("Background Canvas").GetComponentInChildren<Image>().overrideSprite = backgrounds[PlayerPrefs.GetInt("Background S")];
            winningSound.Play();
            maxNumber = PlayerPrefs.GetInt("Max Num S");
            if (PlayerPrefs.GetInt("Enemy 1S") == 0)
            {
                prefabEnemiesDisabled[0] = true;
            }
            else { prefabEnemiesDisabled[0] = false; }
            if (PlayerPrefs.GetInt("Enemy 2S") == 0)
            {
                prefabEnemiesDisabled[1] = true;

            }
            else { prefabEnemiesDisabled[1] = false; }
            if (PlayerPrefs.GetInt("Enemy 3S") == 0)
            {
                prefabEnemiesDisabled[2] = true;

            }
            else { prefabEnemiesDisabled[2] = false; }
            if (PlayerPrefs.GetInt("Enemy 4S") == 0)
            {
                prefabEnemiesDisabled[3] = true;

            }
            else { prefabEnemiesDisabled[3] = false; }
            if (PlayerPrefs.GetInt("Enemy 5S") == 0)
            {
                prefabEnemiesDisabled[4] = true;

            }
            else { prefabEnemiesDisabled[4] = false; }
            silver = false;
            enemySpawnPerSecond = 1.0f;
        }
        else if (setUp)
        {
            GameObject.Find("Game Level").GetComponent<Text>().text = "Bronze";
            GameObject.Find("Background Canvas").GetComponentInChildren<Image>().overrideSprite = backgrounds[PlayerPrefs.GetInt("Background B")];
            maxNumber = PlayerPrefs.GetInt("Max Num B");
            if (PlayerPrefs.GetInt("Enemy 1B") == 0)
            {
                prefabEnemiesDisabled[0] = true;
            }
            else { prefabEnemiesDisabled[0] = false; }
            if (PlayerPrefs.GetInt("Enemy 2B") == 0)
            {
                prefabEnemiesDisabled[1] = true;

            }
            else { prefabEnemiesDisabled[1] = false; }
            if (PlayerPrefs.GetInt("Enemy 3B") == 0)
            {
                prefabEnemiesDisabled[2] = true;

            }
            else { prefabEnemiesDisabled[2] = false; }
            if (PlayerPrefs.GetInt("Enemy 4B") == 0)
            {
                prefabEnemiesDisabled[3] = true;

            }
            else { prefabEnemiesDisabled[3] = false; }
            if (PlayerPrefs.GetInt("Enemy 5B") == 0)
            {
                prefabEnemiesDisabled[4] = true;

            }
            else { prefabEnemiesDisabled[4] = false; }
            setUp = false;

            enemySpawnPerSecond = 5f;
        }
    }
    
    //Increases the number of enemies
    private void UpdateNumOfEnemies(int typeOfEnemy)
    {
        numOfEnemiesKilled[typeOfEnemy]++;
        UpdateScreen();
    }
    private void UpdateScreen()
    {
        GameObject.Find("Total").GetComponent<Text>().text = SumUpEnemiesKilled().ToString();
        GameObject.Find("E1").GetComponent<Text>().text = numOfEnemiesKilled[0].ToString();
        GameObject.Find("E2").GetComponent<Text>().text = numOfEnemiesKilled[1].ToString();
        GameObject.Find("E3").GetComponent<Text>().text = numOfEnemiesKilled[2].ToString();
        GameObject.Find("E4").GetComponent<Text>().text = numOfEnemiesKilled[3].ToString();
        GameObject.Find("E5").GetComponent<Text>().text = numOfEnemiesKilled[4].ToString();

    }
    private int SumUpEnemiesKilled()
    {
        int total = 0;
        for (int i = 0; i < numOfEnemiesKilled.Length; i++)
        {
            total += numOfEnemiesKilled[i];
        }
        return total;
    }
    //Used at every Level Change
    private void SetUpEnemies()
    {
        for (int i = 0; i < numOfEnemiesKilled.Length; i++)
        {
            numOfEnemiesKilled[i] = 0;
        }
    }

    void SetEnemyRate() {
        if (SumUpEnemiesKilled() > PlayerPrefs.GetInt("Number of Enemy S") && gold)
        {
            
            enemySpawnPerSecond = 1.2f;
            gold = false;
        }
        else if (SumUpEnemiesKilled() > PlayerPrefs.GetInt("Number of Enemy B") && silver)
        {
          
            enemySpawnPerSecond = 1.0f;
        }
        else if (setUp)
        {
            enemySpawnPerSecond = 5f;

        }

    }

    void Update()
    {
        print(Game.current.shooterSettings.colourEnemy2);
        GameObject.Find("Time").GetComponent<Text>().text = (((int)Time.timeSinceLevelLoad / 60).ToString() + ":" + (((int)Time.timeSinceLevelLoad % 60) / 10).ToString() + (((int)Time.timeSinceLevelLoad % 60) % 10).ToString());
    }
    private void SetColour()
    {
        checkColourSetting("Enemy 1Color", 0);
        checkColourSetting("Enemy 2Color", 1);
        checkColourSetting("Enemy 3Color", 2);
        checkColourSetting("Enemy 4Color", 3);
        checkColourSetting("Enemy 5Color", 4);
    }

    private void checkColourSetting(string playerPref, int enemy)
    {
        if (PlayerPrefs.GetString(playerPref).Equals("Red"))
        {
            changeColour(0, enemy);
        }
        else if (PlayerPrefs.GetString(playerPref).Equals("Blue"))
        {
            changeColour(1, enemy);

        }
        else if (PlayerPrefs.GetString(playerPref).Equals("Yellow"))
        {
            changeColour(2, enemy);

        }
        else if (PlayerPrefs.GetString(playerPref).Equals("Green"))
        {
            changeColour(3, enemy);

        }
        else if (PlayerPrefs.GetString(playerPref).Equals("Purple"))
        {
            changeColour(4, enemy);
        }
    }
    private void changeColour(int color, int enemy)
    {
        foreach (MeshRenderer a in prefabEnemies[enemy].GetComponentsInChildren<MeshRenderer>())
        {
            a.sharedMaterial = enemyMat[color];
        }
    }
    public bool SetEnemyWeapons(Enemy a)
    {
        if ((PlayerPrefs.GetInt("Enemy 1Shoot") == 1) && (a.typeEnemy == 0))
        {
            return true;
        }
        else
        {
            return false;
        }
        if (PlayerPrefs.GetInt("Enemy 2Shoot") == 1 && (a.typeEnemy == 1))
        {
            return true;
        }
        else
        {
            return false;
        }
        if ((PlayerPrefs.GetInt("Enemy 3Shoot") == 1) && (a.typeEnemy == 2))
        {
            return true;
        }
        else
        {
            return false;
        }
        if (PlayerPrefs.GetInt("Enemy 4Shoot") == 1 && (a.typeEnemy == 3))
        {
            return true;
        }
        else
        {
            return false;
        }
        if (PlayerPrefs.GetInt("Enemy 5Shoot") == 1 && (a.typeEnemy == 4))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
