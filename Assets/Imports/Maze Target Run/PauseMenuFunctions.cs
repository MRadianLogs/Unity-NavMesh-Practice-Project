using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuFunctions : MonoBehaviour
{
    private static bool gameIsPaused = false;

    [SerializeField] private GameObject pauseMenuUI = null;
    [SerializeField] private GameObject player = null;

    void Start()
    {
        gameIsPaused = false;    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);//Brings up menu.
        //player.GetComponentInChildren<PlayerGunMechanics>().enabled = false; //TODO: CHANGE TO NAVMESH AGENT MOVEMENT!
        Time.timeScale = 0f; //Stops game.
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);//Hides menu.
        //player.GetComponentInChildren<PlayerGunMechanics>().enabled = true; //TODO: CHANGE TO NAVMESH AGENT MOVEMENT!
        Time.timeScale = 1f; //Resumes game.
        gameIsPaused = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene("StartMenuScene");
    }
}
