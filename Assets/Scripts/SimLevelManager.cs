using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimLevelManager : MonoBehaviour
{
    private void Awake()
    {
        //Resume the game if it was recently paused.
        Time.timeScale = 1f;
    }
}
