using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;


//The static class responsible for saving the values of the games/users and keeping track of certain static objects (dictionary and list)
public static class SaveLoad {
    public static List<Game> savedGames = new List<Game>();
    public static Dictionary<string, Game> gameDictionary = new Dictionary<string, Game>();
    public static bool loaded = false;

    //When starting game, loads all game files
    public static void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            savedGames = (List<Game>)bf.Deserialize(file);
            AddToDictionary();
            file.Close();
        }
        else
        {
            //Creates some accounts to start off with
            Game admin = new Game("admin", "admin");
            admin.firstLogin = false;
            savedGames.Add(admin);

            Game player1 = new Game("kanyewest", "kardashian");
            player1.firstLogin = false;
            savedGames.Add(player1);

            Game player2 = new Game("jayz", "beyonce");
            player2.firstLogin = true;
            savedGames.Add(player2);

            Game player3 = new Game("taylor", "swift");
            player3.firstLogin = false;
            player3.blocked = true;
            savedGames.Add(player3);

            Game player4 = new Game("barack", "obama");
            player4.firstLogin = false;
            player4.blocked = true;
            savedGames.Add(player4);

            Game player5 = new Game("justin", "trudeau");
            player5.firstLogin = false;
            player5.blocked = true;
            savedGames.Add(player5);

            AddToDictionary();

        }

    }
    //Saves list of saved geames serialization
    public static void Save()
    {   //Logs the time when the user logged out of the system. or rather how long it has been since they logged in
        Game.current.loginHistory[Game.current.loginHistory.Count - 1].secondsPlayed = (int)(float)System.DateTime.Now.Subtract(System.DateTime.Parse(Game.current.loginHistory[Game.current.loginHistory.Count - 1].logInTime)).TotalSeconds;
        savedGames.Remove(savedGames.Find(p => { return p.username == Game.current.username; }));//removes saved version of the game with same name
        savedGames.Add(Game.current);   //adds in current game
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, savedGames);
        file.Close();
    }
    //Function adds list of saved accounts to dictionary for better searching and finding
    public static void AddToDictionary()
    {
        for (int i = 0; i < savedGames.Count; i++)
        {
            gameDictionary.Add(savedGames[i].username, savedGames[i]);
        }
    }
}
