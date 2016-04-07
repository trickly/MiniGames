using UnityEngine;
using System.Collections;

public class Audio_Sources : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }
    void OnEnable()
    {



    }
    public void SetUp()
    {
        if (gameObject.name == "Game Sounds Source")
        {

            gameObject.GetComponent<AudioSource>().volume = Game.current.ssSoundEffectsMusicVol;
        }
        else if (gameObject.name == "Background Music Source")
        {
            gameObject.GetComponent<AudioSource>().volume = Game.current.ssBackgroundMusicVol;

        }
        else if (gameObject.name == "Winning Music Source")
        {
            gameObject.GetComponent<AudioSource>().volume = Game.current.ssWinningMusicVol;
        }
    }
}
