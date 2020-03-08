using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [SerializeField] private int difficulty = 100;//Default value only used if game not started from start screen. Issue from game over screen?
    [SerializeField] private float musicVolume = 100; //Default value only used if game not started from start screen. Issue?

    private void Awake()
    {
        if(gameManager != null && gameManager != this) //If a game manager already exists,
        {
            Destroy(gameObject); //This one is not needed! Destroy it Frodo!
        }
        else //Make this one the game manager.
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    public void SetDifficulty(int newDifficulty)
    {
        difficulty = newDifficulty;
    }
    public float GetDifficulty()
    {
        return difficulty;
    }

    public void SetMusicVolume(float newMusicVolume)
    {
        musicVolume = newMusicVolume;
    }
    public float GetMusicVolume()
    {
        return musicVolume;
    }
}
