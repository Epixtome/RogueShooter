using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public static UIController instance;
    public Slider healthSlider;
    public Text healthText, coinText;
    public GameObject deathScreen;
    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeToBlack, fadeOutBlack;

    public string newGameScene, mainMenuScene;
    public GameObject pauseMenu;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        fadeOutBlack = true;
        fadeToBlack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOutBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.b, fadeScreen.color.g, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }

        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.b, fadeScreen.color.g, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                fadeOutBlack = false;
            }
        }
    }

    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }
    
    public void NewGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(newGameScene);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuScene);
    }

    public void ReturnGame()
    {
        LevelManager.instance.PauseUnpause();
    }
}
