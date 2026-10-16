using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class LevelMusicControllerV3 : MonoBehaviour
{
    public static LevelMusicControllerV3 instance;
 
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
        if (HCController.Hardcore) return;
        if (SceneManager.GetActiveScene().name == "Finish")
            LevelMusicControllerV3.instance.GetComponent<AudioSource>().Pause();
        if (SceneManager.GetActiveScene().name == "Level 1")
            LevelMusicControllerV2.instance.GetComponent<AudioSource>().Pause();
        // Replace "Pause" with "Play" if you wish. 
        
    }
}

// HAVE THIS SCRIPT IN AN EMPTY OBJECT IN LEVEL 1 FOR THE MUSIC TO START AT THE RIGHT TIME! 