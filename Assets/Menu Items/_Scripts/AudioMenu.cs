using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
//Class used to control main menu audio screen
public class AudioMenu : MonoBehaviour
{

    Dropdown drop;
    public AudioClip songA;
    public AudioClip songB;
    public AudioClip songC;
    public AudioClip currentSong;
    public GameObject slider;
    public GameObject source;

    // Use this for initialization
    void Awake()
    {
        source = GameObject.Find("Background Music");

        drop = gameObject.GetComponentInChildren<Dropdown>();
        drop.onValueChanged.AddListener(delegate
        {
            MusicChange(drop);
        });
        currentSong = source.GetComponent<AudioSource>().clip;
    }
    void Start()
    {
        source.GetComponent<AudioSource>().volume = Game.current.backgroundMusicVol;
    }
    //Returns values to how they should be
    void OnEnable()
    {
        currentSong = source.GetComponent<AudioSource>().clip; //sets currently playing song

        source.GetComponent<AudioSource>().Pause();
        //sets audio source, dropdown and slider to correct volumes
        source.GetComponent<AudioSource>().volume = Game.current.backgroundMusicVol; 
        slider.GetComponent<Slider>().value = Game.current.backgroundMusicVol;
        drop.value = Game.current.backgroundMusic;

        if (Game.current != null)
        {
            drop.value = Game.current.backgroundMusic;
        }
    }
    //value of the dropdown determines what song to preview and possibly select
    private void MusicChange(Dropdown target)
    {
        switch (target.value)
        {

            case 0:
                currentSong = songA;
                break;
            case 1:
                currentSong = songB;
                break;
            case 2:
                currentSong = songC;
                break;

        }
        source.GetComponent<AudioSource>().clip = currentSong;
        source.GetComponent<AudioSource>().Play();
        StartCoroutine(MusicPreview());
    }

    //Preview function makes use of a Coroutine to preview music for 3 seconds
    private IEnumerator MusicPreview()
    {
        //  GameObject q = GameObject.Find("Background Music Source");
        //q.GetComponent<AudioSource>().SetScheduledEndTime(3);

        yield return new WaitForSeconds(3f);
        source.GetComponent<AudioSource>().Pause();


    }

    //associated with the slider on the screen
    //controls volume, uses preview
    public void volumeBackgroundMusic(Single newVolume)
    {
        source.GetComponent<AudioSource>().volume = slider.GetComponent<Slider>().value;
        source.GetComponent<AudioSource>().UnPause();
        StartCoroutine(MusicPreview());
    }
    //Whenever user exits audio screen, need to disblae things
    //returns normal background music to playing
    //with possibly changed settings 
    void OnDisable()
    {

        switch (Game.current.backgroundMusic)
        {

            case 0:
                source.GetComponent<AudioSource>().clip = songA;
                break;
            case 1:
                source.GetComponent<AudioSource>().clip = songB;
                break;
            case 2:
                source.GetComponent<AudioSource>().clip = songC;
                break;

        }
        source.GetComponent<AudioSource>().volume = Game.current.backgroundMusicVol;
        source.GetComponent<AudioSource>().Play();

    }
    //Confirms selection of the current value of the dropdown and saves the values to the current account
    public void Confirm()
    {
        source.GetComponent<AudioSource>().clip = currentSong;
        Game.current.backgroundMusic = drop.value;
        Game.current.backgroundMusicVol = slider.GetComponent<Slider>().value;
    }
    public void PlayBackgroundMusic()
    {
        if (currentSong == null)
        {
            source.GetComponent<AudioSource>().clip = songA;
        }
        else
        {
            source.GetComponent<AudioSource>().clip = currentSong;
        }
        source.GetComponent<AudioSource>().Play();

    }
}
