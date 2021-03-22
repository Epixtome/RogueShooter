using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public AudioSource levelMusic, gameOverMusic, winMusic;
    public AudioSource[] SFX;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerGameOver()
    {
        levelMusic.Stop();
        gameOverMusic.Play();
    }

    public void PlayLevelWin()
    {
        levelMusic.Stop();
        winMusic.Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        SFX[sfxToPlay].Stop();
        SFX[sfxToPlay].Play();
    }
}
