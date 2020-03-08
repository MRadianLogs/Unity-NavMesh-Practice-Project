using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject levelEndGoal = null;

    private void Awake()
    {
        //Set player settings from gameManager.
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        SetDifficulty(gameManager.GetDifficulty());
        Time.timeScale = 1f;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        levelEndGoal = GameObject.FindGameObjectWithTag("LevelEndGoal");
        StartCoroutine(CheckWinLoseConditions());
    }
    
    IEnumerator CheckWinLoseConditions()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            //If player has run out of health, lose.

            //If player is within a small distance of objective object, win.
            
            if (Vector3.Distance(player.transform.position, levelEndGoal.transform.position) < 5f)
            {
                //End Game. Change scene to game over scene.
                Debug.Log("Goal found!");
                SceneManager.LoadScene("GameOverScene");
            }
            
        }
    }

    //TODO
    public void SetDifficulty(float newDifficulty)
    {
        //Set the players health based on difficulty level.
        //player.GetComponent<ShooterPlayerViewController>().SetMouseSensitivity(newPlayerMouseSensitivity);
    }

}
