using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class LevelMusicController : MonoBehaviour
{
    public static LevelMusicController instance;
 
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Finish")
            LevelMusicController.instance.GetComponent<AudioSource>().Pause();
        // Replace "Pause" with "Play" if you wish. 
    }
}

// HAVE THIS SCRIPT IN AN EMPTY OBJECT IN LEVEL 1 FOR THE MUSIC TO START AT THE RIGHT TIME! 