using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

//The serializable Game class to save user account information
//A list of these are saved in the data file
[Serializable]
public class Game
{

    public static Game current;
    public string username;
    public string password;
    public bool admin;
    //varaibles pertaining to account statuses (blocked, new, normal)
    public bool blocked;
    public bool firstLogin;
    public int loginCounter;

    //needed settings for game customizations
    public int backgroundMusic;
    public float backgroundMusicVol;
    public int backgroundArt;
    //Space shooter specific settings
    public int ssBackgroundMusic;
    public float ssBackgroundMusicVol;
    public int ssWinningMusic;
    public float ssWinningMusicVol;
    public int ssSoundEffectsMusic;
    public float ssSoundEffectsMusicVol;
    //ApplePicker Settings
    public int apHighScore;
    //Memory Game Settings
    public int memSetting;
    //RPS score: 1 = win, -1 = Loss, 0 = tie


    //variables pertaining to user history
    public List<Log> loginHistory;
    public List<GameLog> shooterHistory;
    public List<GameLog> rpsHistory;
    public List<GameLog> memoryHistory;
    public List<GameLog> appleHistory;
    public SpaceShooterSetting shooterSettings;

    //Default values for a user account
    public Game(string u, string p)
    {
        username = u;
        password = p;

        blocked = false;
        loginCounter = 0;
        firstLogin = true;

        backgroundMusicVol = 0.5f;
        backgroundMusic = 0;
        backgroundArt = 0;

        ssBackgroundMusicVol = 0.5f;
        ssWinningMusicVol = 0.5f;
        ssSoundEffectsMusicVol = 0.5f;
        ssBackgroundMusic = 0;
        ssWinningMusic = 0;
        ssSoundEffectsMusic = 0;
        apHighScore = 1000;
        memSetting = 3;
        loginHistory = new List<Log>();
        shooterHistory = new List<GameLog>();
        rpsHistory = new List<GameLog>();
        memoryHistory = new List<GameLog>();
        appleHistory = new List<GameLog>();
        shooterSettings = new SpaceShooterSetting();
        if (username == "admin")
        {
            admin = true;
        }
        else
        {
            admin = false;
        }
    }

}
//Log class used for login information in main history page
[Serializable]
public class Log
{

    public string logInTime;
    public float secondsPlayed;
    public Log() 
    {
        logInTime = "";
        secondsPlayed = 0;
    }

    public Log(string inTIme, float sPlayed)
    {
        logInTime = inTIme;
        secondsPlayed = sPlayed;
    }
}

//Game logging class used for maintaining general information about each user's playthrough with each game

[Serializable]
public class GameLog
{
    public float secondsPlayed;
    public string startTime;
    public int score;
    public int level; // 0=bronze, 1=silver, 2=gold. USED ONLY FOR SPACE SHOOTER
    public GameLog()
    {
        startTime = "";
        score = 0;
        level = 0;
         secondsPlayed = 0;
}

public GameLog(string sTIme, int s)
    {
        startTime = sTIme;
        score = 0;
        level = 0;
        secondsPlayed = s;

    }
}

//Class contains data about the space shooter game settings (colours, number of enemies, etc.)
//Note there are only variables because for STAGE 2, I used PlayerPrefs (Unity's built in system) to store data instead of using static variables
//PlayerPrefs are still used here in STAGE 3 as seen in Main_Menu of the Space Shooter Project
//They don't accept arrays or boolean values. just integers and strings. 
[Serializable]
public class SpaceShooterSetting
{

    //The number of enemy per level
    public int numEnemyB;
    public int numEnemyS;
    public int numEnemyG;

    //the maximum number of enemies in the screen per level
    public int maxNumB;
    public int maxNumS;
    public int maxNumG;

    //the number of enemies of each type of each level
    public int enemy1B;
    public int enemy2B;
    public int enemy3B;
    public int enemy4B;
    public int enemy5B;
    public int enemy1S;
    public int enemy2S;
    public int enemy3S;
    public int enemy4S;
    public int enemy5S;
    public int enemy1G;
    public int enemy2G;
    public int enemy3G;
    public int enemy4G;
    public int enemy5G;

    //property of enemies if they can shoot or not
    public int shootableEnemy1;
    public int shootableEnemy2;
    public int shootableEnemy3;
    public int shootableEnemy4;
    public int shootableEnemy5;

    //enemy colours 
    public string colourEnemy1;
    public string colourEnemy2;
    public string colourEnemy3;
    public string colourEnemy4;
    public string colourEnemy5;

    //the number of points given by each enemy
    public int pointsEnemy1;
    public int pointsEnemy2;
    public int pointsEnemy3;
    public int pointsEnemy4;
    public int pointsEnemy5;

    //the background of each level and the starting screen
    public int backgroundB;
    public int backgroundS;
    public int backgroundG;
    public int background;

    public SpaceShooterSetting()
    {
        //default values for new Space Shoot account
        numEnemyB = 20;
        numEnemyS = 50;
        numEnemyG = 70;
        maxNumB = 6;
        maxNumS = 8;
        maxNumG = 10;

        enemy1B = 1;
        enemy2B = 1;
        enemy3B = 1;
        enemy4B = 1;
        enemy5B = 1;
        enemy1S = 1;
        enemy2S = 1;
        enemy3S = 1;
        enemy4S = 1;
        enemy5S = 1;
        enemy1G = 1;
        enemy2G = 1;
        enemy3G = 1;
        enemy4G = 1;
        enemy5G = 1;

        shootableEnemy1 = 1;
        shootableEnemy2 = 0;
        shootableEnemy3 = 0;
        shootableEnemy4 = 0;
        shootableEnemy5 = 0;
        colourEnemy1 = "Red";
        colourEnemy2 = "Blue";
        colourEnemy3 = "Yellow";
        colourEnemy4 = "Green";
        colourEnemy5 = "Purple";
        pointsEnemy1 = 10;
        pointsEnemy2 = 15;
        pointsEnemy3 = 20;
        pointsEnemy4 = 30;
        pointsEnemy5 = 50;

        backgroundB = 1;
        backgroundS = 2;
        backgroundG = 3;
        background = 1;
    }


}