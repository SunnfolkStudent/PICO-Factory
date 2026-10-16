using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HardcoreMusicPlayerController : MonoBehaviour
{
    public static HardcoreMusicPlayerController instance;

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
        if (!HCController.Hardcore)
        {
            HardcoreMusicPlayerController.instance.GetComponent<AudioSource>().Pause();
        }

        if (SceneManager.GetActiveScene().name == "Finish")
        {
            HardcoreMusicPlayerController.instance.GetComponent<AudioSource>().Pause();
        }
    }
}