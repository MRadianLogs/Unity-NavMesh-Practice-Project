using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuFunctions : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject mainMenuSelection = null;
    [SerializeField] private GameObject settingsMenu = null;
    [SerializeField] private GameObject levelSelectMenu = null;
    [SerializeField] private Slider difficultySlider = null;
    [SerializeField] private Text difficultyValueText = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private Text musicVolumeValueText = null;
    private const string DIFFICULTY = "difficulty";
    private const string MUSICVOLUME = "musicVolume";

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        //Check for player settings.
        if (PlayerPrefs.HasKey(DIFFICULTY)) //If there is player settings stored.
        {
            //Load + Set those settings.
            SetDifficulty(PlayerPrefs.GetInt(DIFFICULTY));
            difficultySlider.value = PlayerPrefs.GetInt(DIFFICULTY);
        }
        if(PlayerPrefs.HasKey(MUSICVOLUME))
        {
            SetVolumeLevel(PlayerPrefs.GetFloat(MUSICVOLUME));
            volumeSlider.value = PlayerPrefs.GetFloat(MUSICVOLUME);
        }
    }

    public void Play()
    {
        OpenLevelSelectMenu();
    }

    public void Quit()
    {
        Application.Quit();//Exits the game.
    }

    public void OpenLevelSelectMenu()
    {
        mainMenuSelection.SetActive(false);
        levelSelectMenu.SetActive(true);
    }
    public void CloseLevelSelectMenu()
    {
        mainMenuSelection.SetActive(true);
        levelSelectMenu.SetActive(false);
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("LevelOneScene");
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene("LevelTwoScene");
    }

    public void LoadLevelThree()
    {
        SceneManager.LoadScene("LevelThreeScene");
    }

    public void OpenSettingsMenu()
    {
        mainMenuSelection.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        mainMenuSelection.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void SetDifficulty(int newDifficulty)
    {
        gameManager.SetDifficulty(newDifficulty);
        difficultyValueText.text = newDifficulty.ToString();
    }

    public void SetDifficultyWSlider()
    {
        int difficulty = (int)difficultySlider.value;
        gameManager.SetDifficulty(difficulty);
        //Update PlayerPrefs.
        PlayerPrefs.SetInt(DIFFICULTY, difficulty);
        difficultyValueText.text = difficulty.ToString();
    }

    public void SetVolumeLevel(float newVolumeLevel)
    {
        //Change volume!
        gameManager.GetComponent<AudioSource>().volume = (newVolumeLevel/100);
        musicVolumeValueText.text = newVolumeLevel.ToString() + "%";
    }

    public void SetVolumeLevelWSlider()
    {
        float volumeLevel = volumeSlider.value;
        //Update PlayerPrefs.
        PlayerPrefs.SetFloat(MUSICVOLUME, volumeLevel);//Change to int?
        gameManager.GetComponent<AudioSource>().volume = (volumeLevel/100);
        musicVolumeValueText.text = volumeLevel.ToString() + "%";
    }
}
