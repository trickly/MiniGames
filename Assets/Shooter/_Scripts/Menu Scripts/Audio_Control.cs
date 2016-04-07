using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Audio_Control : MonoBehaviour {

    //Dropdown references
    public Dropdown backgroundDrop;
    public Dropdown winningDrop;
    public Dropdown effectsDrop;

    //Audio Source references
    public GameObject backgroundSource;
    public GameObject winningSource;
    public GameObject effectsSource;

    //Slider references
    public GameObject backgroundSlider;
    public GameObject winningSlider;
    public GameObject effectsSlider;

    //Self explainable audio clips
    public AudioClip songA;
    public AudioClip songB;
    public AudioClip songC;

    public AudioClip winA;
    public AudioClip winB;

    public AudioClip laserA;
    public AudioClip laserB;

    public AudioClip destroyA;
    public AudioClip destroyB;
    
    public AudioClip menuSong;

    public AudioClip currentBackgroundMusic;
    public AudioClip currentWinningMusic;
    public AudioClip currentLaser;
    public AudioClip currentDestroy;
    void Awake() {
        backgroundSource = GameObject.Find("Background Music Source");
        winningSource = GameObject.Find("Winning Music Source");
        effectsSource = GameObject.Find("Game Sounds Source");
        
                
    }
    void OnEnable() {
        GameObject.Find("Background Music").GetComponent<AudioSource>().Pause(); //pauses current background music

        //sets the reference of currently saved audio clips so when exiting without saving, they can return to those clips
        currentBackgroundMusic = backgroundSource.GetComponent<AudioSource>().clip;
        currentWinningMusic = winningSource.GetComponent<AudioSource>().clip;
        currentLaser = effectsSource.GetComponents<AudioSource>()[1].clip;
        currentDestroy = effectsSource.GetComponents<AudioSource>()[2].clip;

        //Sets the sliders to the correctly saved values
        backgroundSlider.GetComponent<Slider>().value = Game.current.backgroundMusicVol;
        winningSlider.GetComponent<Slider>().value = Game.current.ssWinningMusicVol;
        effectsSlider.GetComponent<Slider>().value = Game.current.ssSoundEffectsMusicVol;

        //Sets the audio sources to the correct volumes
        backgroundSource.GetComponent<AudioSource>().volume = Game.current.ssBackgroundMusicVol;
        winningSource.GetComponent<AudioSource>().volume = Game.current.ssWinningMusicVol;
        effectsSource.GetComponents<AudioSource>()[1].volume = Game.current.ssSoundEffectsMusicVol;
        effectsSource.GetComponents<AudioSource>()[2].volume = Game.current.ssSoundEffectsMusicVol;

        //Sets the dropdowns to the right values
        backgroundDrop.value = Game.current.ssBackgroundMusic;
        winningDrop.value = Game.current.ssWinningMusic;
        effectsDrop.value = Game.current.ssSoundEffectsMusic;

    }
    
    void OnDisable() {
        backgroundSource.GetComponent<AudioSource>().Stop(); // backgroundSource is the only source that loops so it needs to stop
        GameObject.Find("Background Music").GetComponent<AudioSource>().Play();
        RevertBack();
    }
    //Changes Couroullater allows a preview of music clip given any audio source
    private IEnumerator MusicPreview(AudioSource source)
    {
        yield return new WaitForSeconds(3f);
        source.GetComponent<AudioSource>().Pause();
    }

    //Activated whenever the values on the screen and the audio sources need to return to SAVED values
    public void RevertBack()
    {
        //Switch statements for each set of audio sources
        switch (Game.current.ssBackgroundMusic)
        {
            case 0:
                backgroundSource.GetComponent<AudioSource>().clip = songA;
                break;
            case 1:
                backgroundSource.GetComponent<AudioSource>().clip = songB;
                break;
            case 2:
                backgroundSource.GetComponent<AudioSource>().clip = songC;
                break;
        }
        backgroundSource.GetComponent<AudioSource>().volume = Game.current.ssBackgroundMusicVol;

        switch (Game.current.ssSoundEffectsMusic)
        {
            case 0:
                effectsSource.GetComponents<AudioSource>()[1].clip = laserA;
                effectsSource.GetComponents<AudioSource>()[2].clip = destroyA;
                break;
            case 1:
                effectsSource.GetComponents<AudioSource>()[1].clip = laserA;
                effectsSource.GetComponents<AudioSource>()[2].clip = destroyB;
                break;
        }
        effectsSource.GetComponents<AudioSource>()[1].volume = Game.current.ssSoundEffectsMusicVol;
        effectsSource.GetComponents<AudioSource>()[2].volume = Game.current.ssSoundEffectsMusicVol;

        switch (Game.current.ssWinningMusic)
        {
            case 0:
                winningSource.GetComponent<AudioSource>().clip = winA;
                break;
            case 1:
                winningSource.GetComponent<AudioSource>().clip = winB;
                break;
        }
        winningSource.GetComponent<AudioSource>().volume = Game.current.ssWinningMusicVol;


    }

    //SAVE FUNCTION. sets the value of each variable for future use. Ties to Game 
    public void Save()
    {
        backgroundSource.GetComponent<AudioSource>().clip = currentBackgroundMusic;
        winningSource.GetComponent<AudioSource>().clip = currentWinningMusic;
        effectsSource.GetComponents<AudioSource>()[1].clip = currentLaser;
        effectsSource.GetComponents<AudioSource>()[2].clip = currentDestroy;

        Game.current.ssBackgroundMusic = backgroundDrop.value;
        Game.current.ssSoundEffectsMusic = effectsDrop.value;
        Game.current.ssWinningMusic = winningDrop.value;

        Game.current.ssBackgroundMusicVol = backgroundSlider.GetComponent<Slider>().value;
        Game.current.ssSoundEffectsMusicVol = effectsSlider.GetComponent<Slider>().value;
        Game.current.ssWinningMusicVol = winningSlider.GetComponent<Slider>().value;

    }

    //Changes the audio clip based on the index of the dropdown menu / does not save
    public void BackgroundMusicChange(int change)
    {
        switch (change)
        {
            case 0:
                currentBackgroundMusic = songA;
                break;
            case 1:
                currentBackgroundMusic = songB;
                break;
            case 2:
                currentBackgroundMusic = songC;
                break;
        }
        backgroundSource.GetComponent<AudioSource>().clip = currentBackgroundMusic;
        backgroundSource.GetComponent<AudioSource>().Play();
        StartCoroutine(MusicPreview(backgroundSource.GetComponent<AudioSource>()));
    }
    //Changes the audio clip based on the index of the dropdown menu / does not save
    public void WinningMusicChange(int change)
    {
        switch (change)
        {
            case 0:
                currentWinningMusic = winA;
                break;
            case 1:
                currentWinningMusic = winB;
                break;
        }
        winningSource.GetComponent<AudioSource>().clip = currentWinningMusic;
        winningSource.GetComponent<AudioSource>().Play();
        StartCoroutine(MusicPreview(winningSource.GetComponent<AudioSource>()));
    }
    //Changes the audio clip based on the index of the dropdown menu / does not save
    public void GameSoundChange(int change)
    {
        switch (change)
        {

            case 0:
                currentLaser = laserA;
                currentDestroy = destroyA;

                break;
            case 1:
                currentDestroy = destroyB;
                currentLaser = laserB;

                break;
        }
        effectsSource.GetComponents<AudioSource>()[1].clip = currentLaser;
        effectsSource.GetComponents<AudioSource>()[1].Play();
        effectsSource.GetComponents<AudioSource>()[2].clip = currentDestroy;
        effectsSource.GetComponents<AudioSource>()[2].Play();

        StartCoroutine(MusicPreview(effectsSource.GetComponent<AudioSource>()));
    }

    //Changes the volume of each audio source bsaed on the slider value / does not save
    public void volumeBackgroundMusic(Single newVolume) {
        backgroundSource.GetComponent<AudioSource>().volume = newVolume;
        backgroundSource.GetComponent<AudioSource>().Play();
        StartCoroutine(MusicPreview(backgroundSource.GetComponent<AudioSource>()));
    }
    //Changes the volume of each audio source bsaed on the slider value / does not save
    public void volumeGameSounds(Single newVolume)
    {
        
        effectsSource.GetComponents<AudioSource>()[1].volume = newVolume;
        effectsSource.GetComponents<AudioSource>()[2].volume = newVolume;
        effectsSource.GetComponents<AudioSource>()[1].Play();

    }
    //Changes the volume of each audio source bsaed on the slider value / does not save
    public void volumeWinningMusic(Single newVolume)
    {
       winningSource.GetComponent<AudioSource>().volume = newVolume;
       winningSource.GetComponent<AudioSource>().Play();

    }


    //Obsolete function. might bring it back some day...
    public void PlayBackgroundMusic()
    {/*
        if (currentBackgroundMusic == null)
        {
            backgroundSource.GetComponent<AudioSource>().clip = songA;
        }
        else
        {
            backgroundSource.GetComponent<AudioSource>().clip = currentBackgroundMusic;
        }
        backgroundSource.GetComponent<AudioSource>().Play();
        */
    }
}
